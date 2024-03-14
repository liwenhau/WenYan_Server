<template>
  <a-modal
    width="90%"
    draggable
    v-model:visible="visible"
    modal-animation-name="el-fade"
    :modal-style="{ maxWidth: '500px' }"
    :footer="false"
    @close="close"
  >
    <template #title>
      <a-tag color="blue">
        <template #icon>
          <icon-cloud />
        </template>
        {{ props.title }}
      </a-tag>
    </template>
    <a-upload draggable :directory="props.isDirectory" :multiple="!props.isDirectory" :custom-request="uploadFile" />
  </a-modal>
</template>

<script setup lang="ts">
import { Message, Notification } from '@arco-design/web-vue'
import type { RequestOption, UploadRequest } from '@arco-design/web-vue/es/upload'
import SparkMD5 from 'spark-md5'
import { uploadFileChunk, mergeFile, checkFile } from '@/apis/file'
import type { AllRequstDataItem, taskArrItem } from '@/apis'
interface Props {
  isDirectory: boolean
  title: String
  uploadDir: String
  onClose: Function
  onRefresh: Function
}
const props = withDefaults(defineProps<Props>(), {})
const visible = ref(false)
const refresh = ref(false)
//任务池
const taskArr = ref<Array<taskArrItem>>([])
//最多任务数量
let maxTask = 6
const sparkMD5 = new SparkMD5.ArrayBuffer()
const chunkSize = 1024 * 1024 * 3 // 每个切片的大小定位3m

onMounted(() => {
  visible.value = true
})

const close = () => {
  visible.value = false
}
//先计算文件MD5
//并发上传文件分片
//上传文件
const uploadFile = (option: RequestOption) => {
  refresh.value = false
  async function fileAsc() {
    const file = option.fileItem.file
    if (file) {
      const firstFile = file.slice(0 * chunkSize, 1 * chunkSize)
      const chunkNum = Math.ceil(file.size / chunkSize)
      let taskArrItem: taskArrItem = {
        md5: '',
        name: option.fileItem.name,
        state: 0,
        fileSize: file.size,
        allRequstData: [],
        whileRequests: [],
        finishNumber: 0,
        errNumber: 0,
        option: option
      }
      try {
        //标记文件处理中
        taskArrItem.state = 1
        if (chunkNum === 1) {
          await loadNext(firstFile)
        } else {
          const endFile = file.slice((chunkNum - 1) * chunkSize, (chunkNum - 1 + 1) * chunkSize)
          await loadNext(firstFile)
          await loadNext(endFile)
        }
        const md5 = sparkMD5.end()
        await checkFile(md5)
          .then((res) => {
            if (!res.data) {
              taskArrItem.md5 = md5
              //校验文件秒传
              taskArrItem = createFileChunk(file, chunkNum, taskArrItem)
              //标记文件上传中
              taskArrItem.state = 2
              taskArr.value.push(taskArrItem)
              uploadChunk(taskArrItem)
            } else {
              taskArrItem.option.onSuccess()
              Notification.success({
                content: `文件：${taskArrItem.name}，上传成功！`,
                duration: 5000
              })
            }
          })
          .catch((err) => {
            Message.error(`校验文件发生异常：${err}`)
          })
      } catch (err) {}
    }
  }
  fileAsc()
  const u: UploadRequest = {
    //取消上传事件
    abort: () => {}
  }
  return u
}
const loadNext = (park: Blob) => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.readAsArrayBuffer(park)
    reader.onload = (e) => {
      if (e.target) {
        sparkMD5.append(e.target.result as ArrayBuffer)
      }
      resolve(() => {})
    }
    reader.onerror = (err) => {
      reject(err)
    }
  })
}
//获取文件分片
const createFileChunk = (file: File, chunkNum: number, taskArrItem: taskArrItem) => {
  let cur = 0
  let index = 0
  while (cur < file.size) {
    const sliceFile = file.slice(cur, cur + chunkSize)
    const allRequstData: AllRequstDataItem = {
      file: sliceFile,
      sliceFileSize: sliceFile.size,
      index: index,
      fileSize: file.size,
      fileName: file.name,
      sliceNumber: chunkNum
    }
    taskArrItem.allRequstData.push(allRequstData)
    cur += chunkSize
    index++
  }
  return taskArrItem
}
//上传文件分片
const uploadChunk = async (taskArrItem: taskArrItem) => {
  if (taskArrItem.allRequstData.length === 0 || taskArrItem.whileRequests.length > 0) {
    return
  }
  const isTaskArrIng = toRaw(taskArr.value).filter((item) => item.state === 1 || item.state === 2)
  maxTask = Math.ceil(6 / isTaskArrIng.length) // 实时动态获取并发请求数,每次掉请求前都获取一次最大并发数
  const whileRequest = taskArrItem.allRequstData.slice(-maxTask)
  taskArrItem.allRequstData.length > maxTask
    ? (taskArrItem.allRequstData.length = taskArrItem.allRequstData.length - maxTask)
    : (taskArrItem.allRequstData.length = 0)
  taskArrItem.whileRequests.push(...whileRequest)
  for (const item of whileRequest) {
    request(item)
  }

  async function request(requstDataItem: AllRequstDataItem) {
    const { fileName, file, sliceNumber, index } = requstDataItem
    let form = new FormData()
    form.append('Name', fileName)
    form.append('Chunk', String(index))
    form.append('File', file as File)
    form.append('Md5', taskArrItem.md5)
    form.append('TotalChunk', String(sliceNumber))
    await uploadFileChunk(form).then((res) => {
      if (res.data) {
        taskArrItem.finishNumber++
        const percent = parseInt(((taskArrItem.finishNumber / sliceNumber) * 100).toFixed(2)) / 100
        taskArrItem.option.onProgress(percent)
        taskArrItem.whileRequests = taskArrItem.whileRequests.filter((item) => item.index !== requstDataItem.index)
        //文件上传完成数量
        if (taskArrItem.finishNumber === sliceNumber) {
          taskArrItem.option.onSuccess()
          //合并文件
          const form = reactive({
            md5: taskArrItem.md5,
            fileName: fileName,
            fileDirPath: props.uploadDir,
            fileSize: taskArrItem.fileSize
          })
          mergeFile(form).then(() => {
            Notification.success({
              content: `文件：${fileName}，上传成功！`,
              duration: 5000
            })
            //通知刷新
            refresh.value = true
            props.onRefresh && props.onRefresh(refresh.value)
          })
        } else {
          uploadChunk(taskArrItem)
        }
      }
    })
  }
}
</script>
