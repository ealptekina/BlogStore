using BlogStore.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            // Kategori adı boş geçilemez
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Lütfen kategori adını giriniz");

            // Minimum 3 karakter olmalı
            RuleFor(x => x.CategoryName)
                .MinimumLength(3)
                .WithMessage("Lütfen en az 3 karakter veri girişi yapınız");

            // Maksimum 30 karakter olmalı
            RuleFor(x => x.CategoryName)
                .MaximumLength(30)
                .WithMessage("Lütfen en fazla 30 karakter veri girişi yapınız");
        }
    }
}
