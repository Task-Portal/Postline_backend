using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;


namespace Service
{
    public class PointService:IPointService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        
        
        
        public PointService( IRepositoryManager repository, IMapper mapper,UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        public async Task<PointDto> CreatePointAsync(PointForCreationDto point, string name, bool trackChanges)
        {
            var user = await _userManager.FindByNameAsync(name);
         var post =  await _repository.Post.GetPostWithDetailsAsync(point.PostId, true);   
         var author = post.User.Id;
         if (user.Id==author)
         {
             return new PointDto { IsSuccessful = false, Message = "You can't increase or decrease your own post rating." };
         }

         // check if user already added response to this 
         var isVoteExists =await _repository.Point.GetPointByPostIdAndUserIdAsync(user.Id, post.Id, true);
         
         
         if ( isVoteExists !=null)
         {
             return new PointDto { IsSuccessful = false, Message = "You have already voted."};
         }

         
        var number= _repository.Point.GetNumberByPostId(point.PostId,trackChanges);
         _repository.Point.CreatePoint(new Point{IsIncrement = point.IsIncrement,Post = post,User = user});
         await _repository.SaveAsync();

          return  new PointDto { IsSuccessful = true, Count = number };
        }

      
    }
}