using IG.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Data
{
    public class DataOperations
    {
        public void InsertHierarchyNode(HierarchyNode hierarchyNode)
        {
            try
            {
                using (var context = new IgContext())
                {
                    context.HierarchyNodes.Add(hierarchyNode);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void InsertHierarchyNodes(List<HierarchyNode> hierarchyNodes)
        {
            try
            {
                using (var context = new IgContext())
                {
                    context.HierarchyNodes.AddRange(hierarchyNodes);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
