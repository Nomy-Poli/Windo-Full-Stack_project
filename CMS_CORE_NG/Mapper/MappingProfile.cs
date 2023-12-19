using AutoMapper;
using ModelService;
using ModelService.busoftModels;
using ModelService.windoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windo.Controllers;
using Windo.Models;

namespace Windo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //הראשון מקור השני יעד
            CreateMap<Buisness, BuisnessVm>()
                .ForMember(dest=> dest.ownerName , opt=> opt.MapFrom(src=> src.isdisplayBusinessOwnerName==true? src.User.Firstname: ""))
                .ForMember(dest=> dest.lastupdatedStartDate, opt=> opt.MapFrom(src => src.BuisnessStatus.FirstOrDefault().startDate))
                .ForMember(dest=> dest.userFullName, opt=> opt.MapFrom(src => src.User !=null? src.User.Firstname +" " + src.User.Lastname :""))
            .ForMember(dest => dest.buisnessAreaList1, opt => opt.MapFrom(src => src.BuisnessArea));
            CreateMap<Buisness, BusinessForCardVM>()
                    .ForMember(dest => dest.ownerName, opt => opt.MapFrom(src => src.isdisplayBusinessOwnerName == true ? src.User.Firstname : ""))
                    .ForMember(dest => dest.lastupdatedStartDate, opt => opt.MapFrom(src => src.BuisnessStatus.FirstOrDefault().startDate))
                    .ForMember(dest => dest.buisnessAreaList1, opt => opt.MapFrom(src => src.BuisnessArea));
            CreateMap<BusinessForCardVM, Buisness>();
                    //.ForMember(dest => dest.ownerName, opt => opt.MapFrom(src => src.isdisplayBusinessOwnerName == true ? src.User.Firstname : ""))
                    //.ForMember(dest => dest.lastupdatedStartDate, opt => opt.MapFrom(src => src.BuisnessStatus.FirstOrDefault().startDate))
                    //.ForMember(dest => dest.buisnessAreaList1, opt => opt.MapFrom(src => src.BuisnessArea));
            CreateMap<BuisnessVm, Buisness>();
            CreateMap<Buisness, BusinessNamesPicturesVM>().ReverseMap();
            CreateMap<Buisness, BusinessNamesPicUserIdVM>().ReverseMap();
            CreateMap<PaidTransaction, PaidTransactionVM>().ReverseMap();
            CreateMap<BarterDeal, BarterDealVM>().ReverseMap();
            CreateMap<JointProject, JointProjectVM>()
                .ForMember(dest => dest.BusinessesInCollaboration, opt => opt.MapFrom(src => src.BuisnessesInCollaborations));
            CreateMap<JointProjectVM, JointProject>()
                .ForMember(dest => dest.BuisnessesInCollaborations, opt => opt.MapFrom(src => src.BusinessesInCollaboration));
            CreateMap<CollaborationType, CollaborationTypeVM>().ReverseMap();
            CreateMap<BusinessInCollaborationVM, BusinessInCollaboration>();
            CreateMap<BusinessInCollaboration, BusinessInCollaborationVM>()
                .ForMember(dest => dest.buisnessName, opt => opt.MapFrom(src => src.Business.buisnessName))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Business.Id))
                .ForMember(dest => dest.logoPictureId, opt => opt.MapFrom(src => src.Business.logoPictureId));
            //.ReverseMap();
            CreateMap<CaseStudyPicture, CaseStudyPictureVM>().ReverseMap();
            CreateMap<CaseStudyCustomerResponses, CaseStudyCustomerResponsesVM>().ReverseMap();
            CreateMap<CaseStudy, CaseStudyVM>()
                .ForMember(dest => (int)dest.FromTable, opt => opt.MapFrom(src => src.FromTable))
                .ForMember(dest => dest.CaseStudyPictures, opt => opt.MapFrom(src => src.Pictures))
                .ForMember(dest => dest.CustomerResponses, opt => opt.MapFrom(src => src.CustomerResponseses));
            CreateMap<CaseStudy, CaseStudyForCardsVM>()
                .ForMember(dest => (int)dest.FromTable, opt => opt.MapFrom(src => src.FromTable))
                .ForMember(dest => dest.CaseStudyPictures, opt => opt.MapFrom(src => src.Pictures));
            CreateMap<CaseStudy, CSNamePictureVM>();
            CreateMap<CaseStudyVM, CaseStudy>()
                .ForMember(dest => dest.FromTable, opt => opt.MapFrom(src => (int)src.FromTable))
                .ForMember(dest => dest.CustomerResponseses, opt => opt.MapFrom(src => src.CustomerResponses));
            CreateMap<BusinessInCaseStudy, BusinessInCaseStudyVM>()
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Buisness));
            CreateMap<BusinessInCaseStudyVM, BusinessInCaseStudy>()
                .ForMember(dest => dest.Buisness, opt => opt.MapFrom(src => src.Business));
            CreateMap<CaseStudyPicture, CaseStudyPictureVM>().ReverseMap();
            CreateMap<Message, MessageVM>()
                .ForMember(dest => (int?)dest.CollaborationType, opt => opt.MapFrom(src => src.CollaborationType));
            CreateMap<MessageVM, Message>()
                .ForMember(dest => dest.CollaborationType, opt => opt.MapFrom(src => (int?)src.CollaborationType)); 
            CreateMap<MessagesTo, MessagesToVM>().ReverseMap();
            CreateMap<Note, NoteVM>().ReverseMap();
            CreateMap<Note, NoteWithReplayVM>().ReverseMap();
            CreateMap<Note, NoteForCardVM>().ReverseMap();

            CreateMap<NoteVM, Note>();
            CreateMap<Note, NoteVM>()
                .ForMember(dest => dest.Boards, opt => opt.MapFrom(src => src.NotesBoards.Select(nb=>nb.Board).ToList()))
                .ForMember(dest => dest.ReplayCount, opt => opt.MapFrom(src => src.ReplayToNotes.Count())) ;
            CreateMap<Board, BoardVM>().ReverseMap();
            CreateMap<Board, BoardForCardVM>()
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.NotesBoards.Select(nb => nb.Note))) ;
            CreateMap<BoardForCardVM, Board>();
            CreateMap<NotesBoards, NotesBoardsVM>().ReverseMap();
            CreateMap<ReplayNoteMessage, ReplayNoteMessageVM>().ReverseMap();
            CreateMap<NoteReplay, NoteReplayVM>().ReverseMap();
            CreateMap<OrderService, OrderServiceVM>().ReverseMap();
            CreateMap<OrderService, OrderServiceWithAdDetailsVM>().ReverseMap();
            CreateMap<RequestOrderService, RequestOrderServiceVM>()
                .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => (RequestStatus)src.RequestStatus));
            CreateMap<RequestOrderServiceVM, RequestOrderService>()
                .ForMember(dest => dest.RequestStatus, opt => opt.MapFrom(src => (int)src.RequestStatus));
            CreateMap<AdvertismentServiceOrder, AdvertismentServiceOrderVM>().ReverseMap();
            CreateMap<Banner,BannerVM>().ReverseMap();
            CreateMap<Client,ClientVM>().ReverseMap();
            CreateMap<ClientType, ClientTypeVM>().ReverseMap();
            CreateMap<ServiceType, ServiceTypeVM>().ReverseMap();
            CreateMap<OrderStatuses, OrderStatusesVM>().ReverseMap();
            CreateMap<CatalogService, CatalogServiceVM>().ReverseMap();

            //CreateMap<Buisness, BuisnessWithFilesVm>().ReverseMap();
            CreateMap<Category, CategoryVm>().ReverseMap();
            CreateMap<SubCategory, SubCategoryVm>().ReverseMap();
            CreateMap<BuisnessArea, BuisnessAreaVm>().ReverseMap();
            CreateMap<CategorySubCategory, CategorySubCategoryVm>().ReverseMap();
            CreateMap<BusinessCategoryNotify, BusinessCategoryNotifyVM>().ReverseMap();
            CreateMap<ScroingOperation, ScroingOperationVM>().ReverseMap();
            CreateMap<Buisness, BusinessForScoringVM>().ReverseMap();
            CreateMap<BusinessScoring, BusinessScoringsDetailVM>();
            // .ForMember(dest => dest.ScoringAction, opt => opt.MapFrom(src => (ScoringAction)src.ScoringAction));
            CreateMap<Area, AreaVm>().ReverseMap();            
            CreateMap<ImportResponse, ImportResponseModel>().ReverseMap();
            CreateMap<NetworkingGroup, NetworkingGroupVM>();
            CreateMap<NetworkingGroupVM, NetworkingGroup>();
                // .ForMember(dest => dest.ManagerBusiness, opt => opt.MapFrom(src => (Buisness)src.ManagerBusiness));
                
            CreateMap<NetworkingGroupBusiness, NetworkingGroupBusinessVM>().ReverseMap();
            // .ForMember(dest => dest.ImportId, opt => opt.MapFrom(src => src.ImportId))
            // .ForMember(dest => dest.ImporterName, opt => opt.MapFrom(src => src.ImportFileName))
            // .ForMember(dest => dest.ImportFileName, opt => opt.MapFrom(src => src.ImportFileName))
            // .ForMember(dest => dest.TotalRecords, opt => opt.MapFrom(src => src.TotalRecords))
            // .ForMember(dest => dest.ColumnsFound, opt => opt.MapFrom(src => src.ColumnsFound))//להמיר את אורך רשימת הסטרינגים
            // .ForMember(dest => dest.LoadedRecords, opt => opt.MapFrom(src => src.LoadedRecords))
            // .ForMember(dest => dest.DeletedRecords, opt => opt.MapFrom(src => src.DeletedRecords))
            // .ForMember(dest => dest.MarkForDelete, opt => opt.MapFrom(src => src.MarkForDelete))
            // .ForMember(dest => dest.MarkForNew, opt => opt.MapFrom(src => src.MarkForNew))
            // .ForMember(dest => dest.NewLoaded, opt => opt.MapFrom(src => src.NewLoaded))
            // .ForMember(dest => dest.ErroredRecords, opt => opt.MapFrom(src => src.ErroredRecords))
            // .ForMember(dest => dest.NotUpdated, opt => opt.MapFrom(src => src.NotUpdated))
            // .ForMember(dest => dest.NewRecordsNotInDataBase, opt => opt.MapFrom(src => src.NewRecordsNotInDataBase))
            // .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            // .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));
            ////המפוי לא מתיחס לרשימת הנוספים, לא מתייחס לרשימת המחוקים ולא מתייחס לרשימה של כמה רשומות נמצאו
            //.ForMember(dest => dest.Added., opt => opt.MapFrom(src => src.NewRecordsNotInDataBase))
        }
    }
}
//.ForMember(dest => dest.buisnessId, opts => opts.MapFrom(src => src.buisnessId))
//.ForMember(dest => dest.buisnessName, opts => opts.MapFrom(src => src.buisnessName))
//.ForMember(dest => dest.buisnessWebSiteLink, opts => opts.MapFrom(src => src.buisnessWebSiteLink))
//.ForMember(dest => dest.cityId, opts => opts.MapFrom(src => src.cityId))
//.ForMember(dest => dest.countryWide, opts => opts.MapFrom(src => src.countryWide))
//.ForMember(dest => dest.dateStarted, opts => opts.MapFrom(src => src.dateStarted))
//.ForMember(dest => dest.discription, opts => opts.MapFrom(src => src.discription))
//.ForMember(dest => dest.emailAddress, opts => opts.MapFrom(src => src.emailAddress))
//.ForMember(dest => dest.phoneNumber, opts => opts.MapFrom(src => src.phoneNumber))
//.ForMember(dest => dest.pictursList, opts => opts.MapFrom(src => src.pictursList))
//.ForMember(dest => dest.professionalExperienceDesc, opts => opts.MapFrom(src => src.professionalExperienceDesc))
//.ForMember(dest => dest.profileImg, opts => opts.MapFrom(src => src.profileImg))
//.ForMember(dest => dest.subTopicId, opts => opts.MapFrom(src => src.subTopicId))
//.ForMember(dest => dest.address, opts => opts.MapFrom(src => src.address))
//.ForMember(dest => dest.collaborationBuisness, opts => opts.MapFrom(src => src.collaborationBuisness))
//.ForMember(dest => dest.payingBuisness, opts => opts.MapFrom(src => src.payingBuisness))
//.ForMember(dest => dest.burterBuisness, opts => opts.MapFrom(src => src.burterBuisness));
