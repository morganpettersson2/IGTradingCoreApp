using IG.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

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

        public void InsertTimeFrameData()
        {
            try
            {
                List<TimeFrames> timeFrameList = new List<TimeFrames>();
                using (var context = new IgContext())
                {
                    TimeFrames frame1min = new TimeFrames();
                    frame1min.TimeFrameId = 1;
                    frame1min.Name = "1m";
                    timeFrameList.Add(frame1min);

                    TimeFrames frame5min = new TimeFrames();
                    frame5min.TimeFrameId = 2;
                    frame5min.Name = "5m";
                    timeFrameList.Add(frame5min);

                    TimeFrames frame15min = new TimeFrames();
                    frame15min.TimeFrameId = 3;
                    frame15min.Name = "15m";
                    timeFrameList.Add(frame15min);

                    TimeFrames frame30min = new TimeFrames();
                    frame30min.TimeFrameId = 4;
                    frame30min.Name = "30m";
                    timeFrameList.Add(frame30min);

                    TimeFrames frame60min = new TimeFrames();
                    frame60min.TimeFrameId = 5;
                    frame60min.Name = "60m";
                    timeFrameList.Add(frame60min);

                    TimeFrames frame240min = new TimeFrames();
                    frame240min.TimeFrameId = 6;
                    frame240min.Name = "240m";
                    timeFrameList.Add(frame240min);

                    TimeFrames frame1d = new TimeFrames();
                    frame1d.TimeFrameId = 7;
                    frame1d.Name = "1d";
                    timeFrameList.Add(frame1d);

                    TimeFrames frame1w = new TimeFrames();
                    frame1w.TimeFrameId = 8;
                    frame1w.Name = "1w";
                    timeFrameList.Add(frame1w);
                    

                    context.TimeFrames.AddRange(timeFrameList);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<HierarchyNode> GetAllHierarchyNodes()
        {
            List<HierarchyNode> returnList = new List<HierarchyNode>();
            using (var context = new IgContext())
            {
                returnList = context.HierarchyNodes.ToList();
            }

            return returnList;
        }

        public void DeleteAllHierarchyNodes()
        {
            try
            {
                using (var context = new IgContext())
                {
                    context.HierarchyMarkets.RemoveRange(context.HierarchyMarkets.Where(c=> c.Id > 0 ));
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void InsertHierarchyMarkets(List<HierarchyMarket> marketList)
        {
            try
            {
                using (var context = new IgContext())
                {
                    context.HierarchyMarkets.AddRange(marketList);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void InsertHierarchyMarket(HierarchyMarket marketItem)
        {
            try
            {
                using (var context = new IgContext())
                {
                    context.HierarchyMarkets.Add(marketItem);
                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
