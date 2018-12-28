using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IG.Domain
{
    public class HierarchyMarket
    {
        public int Id { get; set; }

        
        public Int64 HierarchyNodeId { get; set; }
        [ForeignKey("HierarchyNodeId")]
        public HierarchyNode HierarchyNode { get; set; }
        
        
        ///<Summary>
        ///Price delay time in minutes
        ///</Summary>
        public int DelayTime { get; set; }
        ///<Summary>
        ///Instrument epic identifier
        ///</Summary>
        public string Epic { get; set; }
        ///<Summary>
        ///Price net change
        ///</Summary>
        public decimal? NetChange { get; set; }
        ///<Summary>
        ///Instrument lot size
        ///</Summary>
        public int LotSize { get; set; }
        ///<Summary>
        ///Instrument expiry period
        ///</Summary>
        public string Expiry { get; set; }
        ///<Summary>
        ///Instrument type
        ///</Summary>
        public string InstrumentType { get; set; }
        ///<Summary>
        ///Instrument name
        ///</Summary>
        public string InstrumentName { get; set; }
        ///<Summary>
        ///Highest price of the day
        ///</Summary>
        public decimal? High { get; set; }
        ///<Summary>
        ///Lowest price of the day
        ///</Summary>
        public decimal? Low { get; set; }
        ///<Summary>
        ///Percentage price change on the day
        ///</Summary>
        public decimal? PercentageChange { get; set; }
        ///<Summary>
        ///Time of last price update
        ///</Summary>
        public string UpdateTime { get; set; }
        ///<Summary>
        ///Bid price
        ///</Summary>
        public decimal? Bid { get; set; }
        ///<Summary>
        ///Offer price
        ///</Summary>
        public decimal? Offer { get; set; }
        ///<Summary>
        ///True if OTC tradeable
        ///</Summary>
        public bool OtcTradeable { get; set; }
        ///<Summary>
        ///True if streaming prices are available, i.e. the market is tradeable and the client holds the necessary access permissions
        ///</Summary>
        public bool StreamingPricesAvailable { get; set; }
        ///<Summary>
        ///Market status
        ///</Summary>
        public string MarketStatus { get; set; }
        ///<Summary>
        ///multiplying factor to determine actual pip value for the levels used by the instrument
        ///</Summary>
        public int ScalingFactor { get; set; }
    }
}