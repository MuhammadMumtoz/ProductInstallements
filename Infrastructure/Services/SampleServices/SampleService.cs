// using AutoMapper;
// using Domain.Dtos;
// using Microsoft.EntityFrameworkCore;
// using Domain.Entities;
// using System.Net;

// namespace Infrastructure.Services.AlbumServices;

// public class AlbumService : IAlbumService
// {
//     private readonly DataContext _context;
//     private readonly IMapper _iMapper;
//     public AlbumService(DataContext context, IMapper iMapper)
//     {
//         _context = context;
//         _iMapper = iMapper;
//     }
//     public async Task<Response<List<GetAlbumDto>>> GetAlbums()
//     {
//         var list = await (
//             from ab in _context.Albums
//             join at in _context.Artists on ab.ArtistId equals at.ArtistId
//             join tr in _context.Tracks on ab.AlbumId equals tr.AlbumId
//             select new GetAlbumDto
//             {
//                 AlbumId = ab.AlbumId,
//                 Title = ab.Title,
//                 ArtistId = at.ArtistId,
//                 ArtistName = at.ArtistName,
//                 TrackId = tr.TrackId,
//                 TrackName = tr.TrackName
//             }
//             ).ToListAsync();
//         return new Response<List<GetAlbumDto>>(list);
//     }
//     public async Task<Response<GetAlbumDto>> GetAlbumById(int id)
//     {
//         var newAlbum = await (
//             from ab in _context.Albums
//             join at in _context.Artists on ab.ArtistId equals at.ArtistId
//             join tr in _context.Tracks on ab.AlbumId equals tr.AlbumId
//             where ab.AlbumId == id
//             select new GetAlbumDto
//             {
//                 ArtistId = at.ArtistId,
//                 ArtistName = at.ArtistName,
//                 AlbumId = ab.AlbumId,
//                 Title = ab.Title,
//                 TrackId = tr.TrackId,
//                 TrackName = tr.TrackName
//             }
//             ).FirstOrDefaultAsync();
//             return new Response<GetAlbumDto>(newAlbum);
//     }

//     public async Task<Response<AddAlbumDto>> InsertAlbum(AddAlbumDto album)
//     {
//         var newAlbum = _iMapper.Map<Album>(album);
//         await _context.Albums.AddAsync(newAlbum);
//         await _context.SaveChangesAsync();
//         return new Response<AddAlbumDto>(album);
//     }

//     public async Task<Response<AddAlbumDto>> UpdateAlbum(AddAlbumDto album)
//     {
//         var find = await _context.Albums.FindAsync(album.AlbumId);
//         find.Title = album.Title;
//         find.ArtistId = album.ArtistId;
//         await _context.SaveChangesAsync();
//         return new Response<AddAlbumDto>(album);
//     }
//     public async Task<Response<string>> DeleteAlbum(int id)
//     {
//         var find = await _context.Albums.FindAsync(id);
//         _context.Albums.Remove(find);
//         var response = await _context.SaveChangesAsync();
//         if(response>0)
//                 return new Response<string>("Category deleted successfully");
//                 return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
//     }
// }