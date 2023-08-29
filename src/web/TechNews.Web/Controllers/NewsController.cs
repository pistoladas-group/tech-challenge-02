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
                ImageSource = "https://picsum.photos/200",
                PublishDate = new DateTime(2023, 08, 01, 10, 30, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "https://picsum.photos/60",
                    Name = "Everton"
                },
            },
            new News {
                Title = "Outra notíciazinha top",
                SubtTitle = "Aquele subtítulo maroto",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "https://picsum.photos/200",
                PublishDate = new DateTime(2023, 07, 15, 12, 00, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "https://picsum.photos/60",
                    Name = "Everton"
                },
            },
            new News {
                Title = "Outra notíciazinha quentíssima",
                SubtTitle = "Aquele arrmaria maina nã",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "https://picsum.photos/200",
                PublishDate = new DateTime(2023, 07, 15, 12, 00, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "https://picsum.photos/60",
                    Name = "Everton"
                },
            },
            new News {
                Title = "Outra notíciazinha quentíssima",
                SubtTitle = "Aquele arrmaria maina nã",
                Description = "çlasdjf çlasjdf lajsdlçfj asljdf lçasjdf asdjfaslkdjflasjdf asl jdfl ajsdlçkjf alsçdj flçasjdl fjasd f",
                ImageSource = "https://picsum.photos/200",
                PublishDate = new DateTime(2023, 07, 15, 12, 00, 00),
                Author = new Author {
                    Email = "teste@email.com",
                    ImageSource = "https://picsum.photos/60",
                    Name = "Everton"
                },
            },
        };

        return View(model);
    }

    public IActionResult Detail()
    {
        var model = new News
        {
            Title = "Notíciazinha quente",
            Description = @"Mussum Ipsum, cacilds vidis litro abertis.  Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Si num tem leite então bota uma pinga aí cumpadi! Detraxit consequat et quo num tendi nada. Praesent malesuada urna nisi, quis volutpat erat hendrerit non. Nam vulputate dapibus.
Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Quem manda na minha terra sou euzis! Interagi no mé, cursus quis, vehicula ac nisi. Viva Forevis aptent taciti sociosqu ad litora torquent.
Copo furadis é disculpa de bebadis, arcu quam euismod magna. Mé faiz elementum girarzis, nisi eros vermeio. Detraxit consequat et quo num tendi nada. Posuere libero varius. Nullam a nisl ut ante blandit hendrerit. Aenean sit amet nisi.
Suco de cevadiss, é um leite divinis, qui tem lupuliz, matis, aguis e fermentis. Aenean aliquam molestie leo, vitae iaculis nisl. Quem num gosta di mé, boa gentis num é. Quem num gosta di mim que vai caçá sua turmis!
Si num tem leite então bota uma pinga aí cumpadi! Todo mundo vê os porris que eu tomo, mas ninguém vê os tombis que eu levo! Quem manda na minha terra sou euzis! Quem num gosta di mé, boa gentis num é.",
            ImageSource = "https://picsum.photos/seed/1/800/400",
            PublishDate = new DateTime(2023, 08, 01, 10, 30, 00),
            Author = new Author
            {
                Email = "teste@email.com",
                ImageSource = "https://picsum.photos/seed/2/50",
                Name = "Everton Brzozowy Alves"
            },
        };

        return View(model);
    }
}
