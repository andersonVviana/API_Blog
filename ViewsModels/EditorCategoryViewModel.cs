using System.ComponentModel.DataAnnotations;

namespace Blog.ViewsModels;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O Nome é Obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Este Campo tem que estare entre 3 e 40 Caracteres")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O Slug é Obrigatório")]
    public string Slug { get; set; }
}