using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transactions = _transactionRepository.GetTransactions();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
            {
                return NotFound();
            }
               

            var transaction = _transactionRepository.GetTransaction(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            return Ok(transaction);

        }

        [HttpPost("add")]
        public IActionResult CreateTransaction([FromForm , Bind("TransactionId,TransactionTitle,Amount,TransactionDescription")] Transaction transactionCreate)
        {
            if (CreateTransaction == null)
            {
                return BadRequest(ModelState);
            }
               
            var transactions = _transactionRepository.GetTransactions()
                .Where(c => c.TransactionTitle.Trim().ToUpper() == transactionCreate.TransactionTitle.TrimEnd().ToUpper()).FirstOrDefault();

            if (transactions != null)
            {
                ModelState.AddModelError("", "Transaction already exists");

                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_transactionRepository.CreateTransaction(transactionCreate))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully Created");

        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateTransaction(int id, [FromForm , Bind("TransactionId,TransactionTitle,Amount,TransactionDescription")]  Transaction updatedTransaction)
        {
            if (UpdateTransaction == null)
            {
                return BadRequest(ModelState);
            }
                

            if (id != updatedTransaction.TransactionId)
            {
                return BadRequest(ModelState);
            }
                

            if (!_transactionRepository.TransactionExists(id))
            {
                return NotFound();
            }
                

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            if (!_transactionRepository.UpdateTransaction(updatedTransaction))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
            {
                return NotFound();
            }

            var transactionToDelete = _transactionRepository.GetTransaction(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            if (!_transactionRepository.DeleteTransaction(transactionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting transaction");
            }

            return NoContent();
        }
    }
}
