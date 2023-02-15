using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IFeedbackRL
    {
        public FeedbackModel AddFeedback(FeedbackModel addFeedback, int userId);
        public List<FeedbackModel> GetAllFeedbacks(int bookId);


    }
}
