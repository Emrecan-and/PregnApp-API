using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PregnAPI.DTO;
using PregnAPI.Models;

namespace PregnAPI.Controllers{
[ApiController]
[Route("api/[controller]")]
public class ArticleController:ControllerBase{
 private readonly PregnAppContext _context;

 public ArticleController(PregnAppContext context)
 {
    _context=context;
 }

[HttpPost]
public async Task<IActionResult> createArticle(ArticleDTO articleDTO){
 if(articleDTO==null){
   return BadRequest();
 }else{

    _context.Articles.Add(new Article{
        ArticleDate=articleDTO.ArticleDate,
        Title=articleDTO.Title,
        Content=articleDTO.Content
    });
    try{await _context.SaveChangesAsync();}catch(Exception error){
        return NotFound();
    }
   return Ok(articleDTO);
 }
}

[HttpGet]
public async Task<IActionResult> GetArticle(){
 ArticleDTO article=await _context.Articles.OrderByDescending(vt=>vt.ArticleDate).Select(vt=>articleToDTO(vt)).FirstOrDefaultAsync();
 if(article==null){
    var errorModel=new ErrorModel(404,"Makale yok veritabanında!");
    return NotFound(errorModel);
 }else{
   return Ok(article);
 }
}


public static ArticleDTO articleToDTO(Article article){
    return new ArticleDTO{
       ArticleDate=article.ArticleDate,
       Title=article.Title,
       Content=article.Content
    };
}

}
}