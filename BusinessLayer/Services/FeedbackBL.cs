using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL:IFeedbackBL
    {
        private readonly IFeedbackRL feedrl;

        public FeedbackBL(IFeedbackRL feedrl)
        {
            this.feedrl = feedrl;
        }


        public FeedbackModel AddFeedback(FeedbackModel addFeedback, int userId)
        {
            try
            {
                return this.feedrl.AddFeedback(addFeedback, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<FeedbackModel> GetAllFeedbacks(int bookId)
        {
            try
            {
                return this.feedrl.GetAllFeedbacks(bookId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
