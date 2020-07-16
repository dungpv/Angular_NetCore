﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.ViewModels;
using KnowledgeSpace.ViewModels.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeSpace.WebPortal.Models
{
    public class ListByTagIdViewModel
    {
        public Pagination<KnowledgeBaseQuickVm> Data { set; get; }

        public LabelVm LabelVm { set; get; }
    }
}