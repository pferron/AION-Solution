using System.Collections.Generic;

namespace AION.BL.Models
{
    public class PlanReviewCycle
    {
        public string ProjectId;
        public List<ReReview> ReReviews;
        public int CycleNumber;
        public int CurrentCycleNumber;
        public int FutureCycleNumber;
        
        public int NextCycleNumber
        {
            get {  
                if (FutureCycleNumber == 0) 
                {
                    return CurrentCycleNumber += 1; 
                } 
                else 
                { 
                    return 0; 
                } 
            }
        }

        public bool HasFutureCycle
        {
            get { 
                if (FutureCycleNumber > 0) 
                { 
                    return true; 
                } 
                else 
                { 
                    return false; 
                } 
            }
        }

        public PlanReviewCycle()
        {
            this.ReReviews = new List<ReReview>();
        }
    }
}