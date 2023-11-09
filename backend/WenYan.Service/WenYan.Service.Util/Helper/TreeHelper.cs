namespace WenYan.Service.Util
{
    public class TreeHelper
    {
        public static List<T> GetTreeList<T>(List<T> allNodes)
    where T : ITreeModel<T>
        {
            var rootData = allNodes.Where(w => w.ParentId.IsNullOrEmpty()).ToList();
            foreach (var children in rootData)
            {
                children.Children = GetChildrenList(allNodes, children);
            }
            return rootData;
        }
        public static List<T> GetChildrenList<T>(List<T> allNodes, T parentNode)
            where T : ITreeModel<T>
        {
            var listChild = allNodes.Where(w => w.ParentId == parentNode.Id).ToList();
            if (listChild == null || listChild.Count == 0) return null;
            foreach (var child in listChild)
            {
                child.Children = GetChildrenList(allNodes, child);
            }
            return listChild;
        }

        public static List<T> GetBaseTreeList<T>(List<T> allNodes)
            where T : BaseTreeModel<T>
        {
            var rootData = allNodes.Where(w => w.ParentId.IsNullOrEmpty()).ToList();
            foreach (var children in rootData)
            {
                children.Children = GetBaseChildrenList(allNodes, children);
            }
            return rootData;
        }
        public static List<T> GetBaseChildrenList<T>(List<T> allNodes, T parentNode)
            where T : BaseTreeModel<T>
        {
            var listChild = allNodes.Where(w => w.ParentId == parentNode.Id).ToList();
            if (listChild == null || listChild.Count == 0) return null;
            foreach (var child in listChild)
            {
                child.Children = GetBaseChildrenList(allNodes, child);
            }
            return listChild;
        }
    }
}
