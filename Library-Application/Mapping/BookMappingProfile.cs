using AutoMapper;
using Library_Application.BookDTO;
using Library_Domain.Model;
using Library_Domain.ValueObject;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<RequestCreateBookDTO, Book>()
            .ConstructUsing(dto => new Book(
                new Title(dto.Title),
                new Author(dto.Author),
                new Genre(dto.Genre),
                dto.PublishedDate,
                dto.Description
            ));

        CreateMap<Book, RequestCreateBookDTO>()
            .ConstructUsing(dto => new RequestCreateBookDTO(
                dto.Title.Value,
                dto.Author.Value,
                dto.Genre.Value,
                dto.PublishedDate.Date,
                dto.Description
                ))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Value))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.Date))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));


        CreateMap<Book, RequestUpdateDTO>()
               .ConstructUsing(dto => new RequestUpdateDTO(
                dto.Title.Value,
                dto.Author.Value,
                dto.Genre.Value,
                dto.PublishedDate.Date,
                dto.Description
                ))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Value))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.Date))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<RequestUpdateDTO, Book>()
            .ConstructUsing(dto => new Book(
                new Title(dto.Title),
                new Author(dto.Author),
                new Genre(dto.Genre),
                dto.PublishedDate,
                dto.Description
            ));

        CreateMap<Book, RequestDeleteDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Book deleted successfully"));

        CreateMap<Book, ResponseBookDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PublishedDate.Date))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
