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
                new PublishedDate(dto.PublishedDate)
            ));
        CreateMap<Book, RequestCreateBookDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.Date));

        CreateMap<Book, RequestUpdateDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.PublishedDate.Date));

        CreateMap<RequestUpdateDTO, Book>()
            .ConstructUsing(dto => new Book(
                new Title(dto.Title),
                new Author(dto.Author),
                new Genre(dto.Genre),
                new PublishedDate(dto.PublishedDate)
        ));

        CreateMap<Book, RequestDeleteDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Book, ResponseBookDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Value))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PublishedDate.Date));
    }
}
