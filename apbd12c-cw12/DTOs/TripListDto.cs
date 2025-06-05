namespace apbd12c_cw12.DTOs;

public class TripListDto
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripDto> Trips { get; set; } = new();
}