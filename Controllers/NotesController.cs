using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models;
using System.Text.Json;

namespace NotesApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _db.Notes
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Priority")] Note note)
        {
            if (ModelState.IsValid)
            {
                note.CreatedAt = DateTime.UtcNow;
                _db.Add(note);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var notes = await _db.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync();
            return View("Index", notes);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AjaxUpdate(int id, [FromBody] JsonElement payload)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();

            if (payload.TryGetProperty("field", out var field) && payload.TryGetProperty("value", out var value))
            {
                var f = field.GetString();
                var v = value.GetString() ?? "";
                if (f == "Title") note.Title = v;
                if (f == "Content") note.Content = v;
                if (f == "Priority" && int.TryParse(v, out var p)) note.Priority = (Priority)p;
                note.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note == null) return NotFound();

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
