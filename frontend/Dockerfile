# 设置基础镜像
FROM nginx
# 定义作者
LABEL MAINTAINER="wenyan" 
# 将dist文件中的内容复制到 /usr/share/nginx/html/ 这个目录下面
COPY dist/ /usr/share/nginx/html/
# 覆盖目录下的default.conf
COPY default.conf /etc/nginx/conf.d/