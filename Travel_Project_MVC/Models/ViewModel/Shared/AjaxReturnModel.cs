using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Travel_Project_MVC.Enum;

namespace Travel_Project_MVC.Models.ViewModel.Shared
{
    public class AjaxReturnModel
    {
        public Guid recordId { get; set; }

        public ReturnStatus ReturnStatus { get; set; }

        private string _ReturnStatusStr;
        public string ReturnStatusStr
        {
            get
            {
                _ReturnStatusStr = ReturnStatus == ReturnStatus.success ? "success" : "error";

                return _ReturnStatusStr;
            }
            set
            {
                _ReturnStatusStr = value;
            }
        }

        private List<string> _ErrorMessages;
        public List<string> ErrorMessages
        {
            get
            {
                if (_ErrorMessages == null)
                    _ErrorMessages = new List<string>();

                return _ErrorMessages;
            }
            set 
            {
                _ErrorMessages = value == null ? new List<string>() : value;
            }
        }
    }
}