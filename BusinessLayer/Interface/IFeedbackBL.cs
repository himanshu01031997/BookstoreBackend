﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(FeedbackModel addFeedback, int userId);
        public List<FeedbackModel> GetAllFeedbacks(int bookId);


    }
}
