using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EditoraDomain.Entities;
using EditoraService;
using EditoraMVC.Models;
using System.Diagnostics;
using Azure.Storage.Blobs;
using System.Reflection.Metadata;
using Azure.Storage.Blobs.Models;
using EditoraAPI.Migrations;
using System.ComponentModel;

namespace EditoraMVC.Controllers
{
    public class LivrosController : Controller
    {
        private readonly EditoraDbContext _context;

        public LivrosController(EditoraDbContext context)
        {
            _context = context;
        }

        // GET: Livros
        public ActionResult Index()
        {
            var livros = _context.livros.Include(l => l.Autores).ToList();

            return View(livros);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.livros == null)
            {
                return NotFound();
            }

            var livro = await _context.livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            var connectionString = "DefaultEndpointsProtocol=https;AccountName=pbinfnet5;AccountKey=ge+96z23tmEU8//RQAV8QFhUp4zb9jhckK5nxfyrPKyspcgISqyfBwVP4CHVm7/Khbg/hVa/cZfQ+AStU6pQVQ==;EndpointSuffix=core.windows.net";
            var containerName = "editora";
            var containerClient = new BlobContainerClient(connectionString, containerName);
            var blobClient = containerClient.GetBlobClient(livro.Capa.NomeArquivo);

            var imageUrl = blobClient.Uri.AbsoluteUri;

            var response = await blobClient.DownloadAsync();
            using var streamReader = new StreamReader(response.Value.Content);
            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            var imagemBytes = memoryStream.ToArray();

            var base64String = Convert.ToBase64String(imagemBytes);
            var imageSrc = string.Format("data:image/gif;base64,{0}", base64String);

            ViewData["Imagem"] = imageSrc;

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,ISBN,Ano")] Livro livro, IFormFile capa)
        {
            if (ModelState.IsValid)
            {
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    await capa.CopyToAsync(stream);
                    bytes = stream.ToArray();
                }

                var imagem = new Imagem
                {
                    Bytes = bytes,
                    NomeArquivo = capa.FileName
                };
                livro.Capa = imagem;

                var fileName = Path.GetFileName(capa.FileName);
                var fileType = Path.GetExtension(fileName);
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileType);

                var connectionString = "DefaultEndpointsProtocol=https;AccountName=pbinfnet5;AccountKey=ge+96z23tmEU8//RQAV8QFhUp4zb9jhckK5nxfyrPKyspcgISqyfBwVP4CHVm7/Khbg/hVa/cZfQ+AStU6pQVQ==;EndpointSuffix=core.windows.net";
                var containerName = "editora";
                var containerClient = new BlobContainerClient(connectionString, containerName);
                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(newFileName);
                using (var stream = new MemoryStream(bytes))
                {
                    await blobClient.UploadAsync(stream);
                }

                var fileUrl = blobClient.Uri.AbsoluteUri;

                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.livros == null)
            {
                return NotFound();
            }

            var livro = await _context.livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,ISBN,Ano")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.livros == null)
            {
                return NotFound();
            }

            var livro = await _context.livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.livros == null)
            {
                return Problem("Entity set 'EditoraDbContext.livros'  is null.");
            }
            var livro = await _context.livros.FindAsync(id);
            if (livro != null)
            {
                _context.livros.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
          return (_context.livros?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public void UploadFile(IFormFile foto)
        {
            if (foto.Length > 0)
            {
                var fileName = Path.GetFileName(foto.FileName);
                var fileType = Path.GetExtension(fileName);
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileType);

                BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=pbinfnet5;AccountKey=ge+96z23tmEU8//RQAV8QFhUp4zb9jhckK5nxfyrPKyspcgISqyfBwVP4CHVm7/Khbg/hVa/cZfQ+AStU6pQVQ==;EndpointSuffix=core.windows.net", "editora");

                BlobClient client = container.GetBlobClient(newFileName);

                using Stream stream = foto.OpenReadStream();
                client.Upload(stream);
                var fileUrl = client.Uri.AbsoluteUri;
            }
        }
    }

}
