using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.Contents
{
    public class KnowledgeBaseCreateRequestValidator : AbstractValidator<KnowledgeBaseCreateRequest>
    {
        public KnowledgeBaseCreateRequestValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0)
                .WithMessage("Danh mục không được để trống.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Tiêu đề không được để trống.");

            RuleFor(x => x.Problem).NotEmpty().WithMessage("Vấn đề không được để trống.");

            RuleFor(x => x.Note).NotEmpty().WithMessage("Giải pháp không được để trống.");
        }
    }
}
