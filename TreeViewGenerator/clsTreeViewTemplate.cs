namespace TreeViewGenerator
{
    public class clsTreeViewTemplate
    {
        public static clsTreeViewTemplate singleton;
        
        public static clsTreeViewTemplate _sharedObject() {
            if (singleton == null) {
                singleton = new clsTreeViewTemplate();
            }
            return singleton;
        }
        public clsTreeViewTemplate() {

        }

        public void _mkTreeViewScript(TableViewModel t, ColumnModel c)
        {
            
           
            
            
        }
        
        
    }
}