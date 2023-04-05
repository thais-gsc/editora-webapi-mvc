using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EditoraDomain.Entities;
using EditoraService;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EditoraMVC.Controllers
{
    public class AutorsController : Controller
    {
        private readonly EditoraDbContext _context;
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EditoraDatabaseAzure;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public AutorsController(EditoraDbContext context)
        {
            _context = context;
        }

        // GET: Autors
        public IActionResult Index()
        {
            var autores = new List<Autor>();

            using (var connection = new SqlConnection(connectionString))
            {
                var procName = "GetAllAutores";
                var sqlCommand = new SqlCommand(procName, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var autor = new Autor
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };

                            autores.Add(autor);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return View(autores);
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.autores == null)
            {
                return NotFound();
            }

            var autor = await _context.autores.Include(a => a.Livros)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            var autores = new List<Autor>();

            using (var connection = new SqlConnection(connectionString))
            {
                var procName = "CreateAutor";
                var sqlCommand = new SqlCommand(procName, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Nome", autor.Nome);
                sqlCommand.Parameters.AddWithValue("@Sobrenome", autor.Sobrenome);
                sqlCommand.Parameters.AddWithValue("@Email", autor.Email);
                sqlCommand.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            var novoAutor = new Autor
                            {
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };

                            autores.Add(novoAutor);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Autors/Edit/5
        public ActionResult Edit(int id)
        {
            Autor autor = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var procName = "GetAutor";
                var sqlCommand = new SqlCommand(procName, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            autor = new Autor
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Autor autor)
        {
            Autor autorUpdate = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var procName = "UpdateAutor";
                var sqlCommand = new SqlCommand(procName, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.Parameters.AddWithValue("@Nome", autor.Nome);
                sqlCommand.Parameters.AddWithValue("@Sobrenome", autor.Sobrenome);
                sqlCommand.Parameters.AddWithValue("@Email", autor.Email);
                sqlCommand.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            autorUpdate = new Autor
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Autor autor = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var procName = "DeleteAutor";
                var sqlCommand = new SqlCommand(procName, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            autor = new Autor
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Sobrenome = reader["Sobrenome"].ToString(),
                                Email = reader["Email"].ToString(),
                                DataNascimento = (DateTime)reader["DataNascimento"]
                            };
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
          return (_context.autores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
