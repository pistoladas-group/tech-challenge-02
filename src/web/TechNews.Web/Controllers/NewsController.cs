using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNews.Web.Models;

namespace TechNews.Web.Controllers;

[Authorize]
public class NewsController : Controller
{
    private readonly ILogger<NewsController> _logger;

    public NewsController(ILogger<NewsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new List<News> {
            new News {
                Title = "Notíciazinha quente",
                SubtTitle = "Aquele subtítulo maroto",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "",
                PublishDate = new DateTime(2023, 08, 01, 10, 30, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "",
                    Name = "Everton"
                },
            },
            new News {
                Title = "Outra notíciazinha top",
                SubtTitle = "Aquele subtítulo maroto",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "",
                PublishDate = new DateTime(2023, 07, 15, 12, 00, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "",
                    Name = "Everton"
                },
            },
            new News {
                Title = "Outra notíciazinha quentíssima",
                SubtTitle = "Aquele arrmaria maina nã",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "",
                PublishDate = new DateTime(2023, 07, 15, 12, 00, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "",
                    Name = "Everton"
                },
            },
        };

        return View(model);
    }
}
