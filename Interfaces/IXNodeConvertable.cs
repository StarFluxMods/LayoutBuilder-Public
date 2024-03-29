namespace LayoutBuilder
{
    public interface IXNodeConvertable
    {
        public void LoadValuesFromXNode(XNode.Node node);
        public T ConvertToXNode<T>() where T : XNode.Node, new();
    }
}