using Api.Dtos.Loans.Requests;
using Api.Dtos.Loans.Responses;
using Application.UseCases.Authors;
using Application.UseCases.Loans;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly CreateLoanUseCase _createLoanUseCase;
    private readonly GetLoanUseCase _getLoanUseCase;
    private readonly ListLoansUseCase _listLoansUseCase;
    private readonly UpdateLoanUseCase _updateLoanUseCase;
    private readonly DeleteLoanUseCase _deleteLoanUseCase;

    public LoanController(
        CreateLoanUseCase createLoanUseCase,
        GetLoanUseCase getLoanUseCase,
        ListLoansUseCase listLoansUseCase,
        UpdateLoanUseCase updateLoanUseCase,
        DeleteLoanUseCase deleteLoanUseCase
        )
    {
        _createLoanUseCase = createLoanUseCase;
        _getLoanUseCase = getLoanUseCase;
        _listLoansUseCase = listLoansUseCase;
        _updateLoanUseCase = updateLoanUseCase;
        _deleteLoanUseCase = deleteLoanUseCase;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateLoan(
        [FromBody] CreateLoanRequestDto request,
        CancellationToken ct
    )
    {
        var command = new CreateLoanCommand(
            request.BookId, request.BorrowerName, request.LoanDate, request.DueDate
        );
        var result = await _createLoanUseCase.Handle(command, ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        var response = new LoanResponseDto
        {
            Id = result.Value,
            BookId = request.BookId,
            BorrowerName = request.BorrowerName,
            LoanDate = request.LoanDate,
            DueDate = request.DueDate
        };
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LoanResponseDto>> GetById(int id, CancellationToken ct)
    {
        var query = new GetLoanQuery(id);
        var result = await _getLoanUseCase.Handler(query, ct);
        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });
        var loan = result.Value;
        var response = new LoanResponseDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            BorrowerName = loan.BorrowerName,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate
        };
        return Ok(response);
    }

    [HttpGet("List")]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetAll(CancellationToken ct)
    {
        var command = new ListLoansQuery();
        var response = await _listLoansUseCase.Handle(command, ct);
        return Ok(response);
    }

}