  // onSubmit() {
  //   // this._wrapperSearchService.logInUserId$.subscribe(res => {
  //   //   this.personalInformation.userId = res;
  //   // }) 
  //   // this.personalInformation.userId = localStorage.getItem("logInUserIdNormaly");//userId
  //   this.personalInformation.id = this.personalInformationForm.get('idBusiness').value;
  //   // this.personalInformation.userId = localStorage.getItem("logInUserId");//userId
  //   this.personalInformation.userId = localStorage.getItem("logInUserEmail");//userId
  //   this.personalInformation.buisnessName = this.personalInformationForm.get('buisnessName').value;//buisnessName 
  //   this.personalInformation.phoneNumber1 = this.personalInformationForm.get('phoneNumber1').value;//1 טלפונים
  //   this.personalInformation.phoneNumber2 = this.personalInformationForm.get('phoneNumber2').value;//2 טלפונים        
  //   this.personalInformation.address = this.personalInformationForm.get('address').value;//כתובת
  //   // this.personalInformation.logoPicture = this.personalInformationForm.get('profpicfile').value;//logoPicture  
  //   this.personalInformation.actionDiscription = this.personalInformationForm.get('actionDiscription').value;//סלוגן
  //   this.personalInformation.discription = this.personalInformationForm.get('discription').value;//תיאור
  //   this.personalInformation.buisnessWebSiteLink = this.personalInformationForm.get('buisnessWebSiteLink').value;//לינק לאתר
  //   this.personalInformation.isdisplayBusinessOwnerName = this.personalInformationForm.get('isdisplayBusinessOwnerName').value;//האם להציג את שם בעלת העסק
  //   this.personalInformation.ispayingBuisness = this.personalInformationForm.get('ispayingBuisness').value;//האם שיטת העסק היא לפי תשלום?
  //   this.personalInformation.isburterBuisness = this.personalInformationForm.get('isburterBuisness').value;//האם העסק הוא בשיטת בארטר?
  //   this.personalInformation.iscollaborationBuisness = this.personalInformationForm.get('iscollaborationBuisness').value;//האם העסק פועל בשת"פ
  //   this.personalInformation.product1 = this.personalInformationForm.get('product1').value;//מוצר ראשון
  //   this.personalInformation.product2 = this.personalInformationForm.get('product2').value;//מוצר שני
  //   this.personalInformation.barterProduct1 = this.personalInformationForm.get('BarterProduct1').value;//מוצר ראשון לבארטר
  //   this.personalInformation.barterProduct2 = this.personalInformationForm.get('BarterProduct2').value;//מוצר שני לבארטר
  //   this.personalInformation.lastupdatedStartDate = null; //תאריך
  //   //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~מילוי הקוגוריות במודל שישלח
  //   //list of subcategory 1
  //   if (this.ModelSubCategory1 != null) {
  //     this.ModelSubCategory1.forEach(ThisCategoryList => {
  //       if (!this.personalInformation.buisnessCategory1) {
  //         this.personalInformation.buisnessCategory1 = [];
  //       }
  //       if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
  //         var Barter = true;
  //       }
  //       //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
  //       else if (this.ModelSubCategoryForBarter1 == null) {
  //         Barter = false;
  //       }
  //       else {
  //         Barter = false;
  //         this.ModelSubCategoryForBarter1.forEach(batrer => {
  //           if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
  //             Barter = true;
  //           }
  //         })
  //       }
  //       // if (this.personalInformationForm.get(''))
  //       this.personalInformation.buisnessCategory1.push({
  //         businessId: null,
  //         categoryId: this.categoryOptionsAfterChoose[0].label,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id,  
  //         isPossibleInBarter: Barter
  //       })
  //     });
  //   }
  //   //list of subcategory 2
  //   if (this.ModelSubCategory2 != null) {
  //     this.ModelSubCategory2.forEach(ThisCategoryList => {
  //       if (!this.personalInformation.buisnessCategory2) {
  //         this.personalInformation.buisnessCategory2 = [];
  //       }
  //       if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
  //         var Barter = true;
  //       }
  //       //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
  //       else if (this.ModelSubCategoryForBarter2 == null) {
  //         Barter = false;
  //       }
  //       else {
  //         Barter = false;
  //         this.ModelSubCategoryForBarter2.forEach(batrer => {
  //           if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
  //             Barter = true;
  //           }
  //         })
  //       }
  //       this.personalInformation.buisnessCategory2.push({
  //         businessId: null,
  //         categoryId: this.categoryOptionsAfterChoose[1].label,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id,
  //         
  //         isPossibleInBarter: Barter
  //       })
  //     });
  //   }
  //   //list of subcategory 3
  //   if (this.ModelSubCategory3 != null) {
  //     this.ModelSubCategory3.forEach(ThisCategoryList => {
  //       if (!this.personalInformation.buisnessCategory3) {
  //         this.personalInformation.buisnessCategory3 = [];
  //       }
  //       if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
  //         var Barter = true;
  //       }
  //       //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
  //       else if (this.ModelSubCategoryForBarter3 == null) {
  //         Barter = false;
  //       }
  //       else {
  //         Barter = false;
  //         this.ModelSubCategoryForBarter3.forEach(batrer => {
  //           if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
  //             Barter = true;
  //           }
  //         })
  //       }
  //       this.personalInformation.buisnessCategory3.push({
  //         businessId: null,
  //         categoryId: this.categoryOptionsAfterChoose[2].label,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id,
  //         
  //         isPossibleInBarter: Barter
  //       })
  //     });
  //   }
  //   //list of subcategory 4
  //   if (this.ModelSubCategory4 != null) {
  //     this.ModelSubCategory4.forEach(ThisCategoryList => {
  //       if (!this.personalInformation.buisnessCategory4) {
  //         this.personalInformation.buisnessCategory4 = [];
  //       }
  //       if (this.personalInformationForm.get('isAllServiceForBurter').value == true) {
  //         var Barter = true;
  //       }
  //       //אם הרשימה ריקה אז היא לא מאפשרת בארטר בכלל
  //       else if (this.ModelSubCategoryForBarter4 == null) {
  //         Barter = false;
  //       }
  //       else {
  //         Barter = false;
  //         this.ModelSubCategoryForBarter4.forEach(batrer => {
  //           if (batrer.CategorySubCategoryId == ThisCategoryList.CategorySubCategoryId) {
  //             Barter = true;
  //           }
  //         })
  //       }
  //       this.personalInformation.buisnessCategory4.push({
  //         businessId: null,
  //         categoryId: this.categoryOptionsAfterChoose[3].label,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id,
  //         
  //         isPossibleInBarter: Barter
  //       })
  //     });
  //   }
  //   //~~~~~~~~~~~~~~barter:
  //   //!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~barter1
  //   if (this.buisnessBarterCategory1 != null) {
  //     this.buisnessBarterCategory1.forEach(ThisCategoryList => {
  //       if (this.personalInformation.buisnessBarterCategory1 == null) {
  //         this.personalInformation.buisnessBarterCategory1 = [];
  //       }
  //       this.personalInformation.buisnessBarterCategory1.push({
  //         businessId: null,
  //         categoryId: this.CForBarter1,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id
  //       })
  //     });
  //   }
  //   //!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~barter2
  //   if (this.buisnessBarterCategory2 != null) {
  //     this.buisnessBarterCategory2.forEach(ThisCategoryList => {
  //       if (!this.personalInformation.buisnessBarterCategory2) {
  //         this.personalInformation.buisnessBarterCategory2 = [];
  //       }
  //       this.personalInformation.buisnessBarterCategory2.push({
  //         businessId: null,
  //         categoryId: this.CForBarter2,
  //         combinationtId: ThisCategoryList.CategorySubCategoryId,
  //         subCategoryId: ThisCategoryList.Id
  //       })
  //     });
  //   }
  //   // מעבר על רשימת הערים שהתקבלה ולהכניס אותם למודל
  //   //אם היא בחרה כל הארץ ועוד דברים נכניס רק כל הארץ
  //   if (this.personalInformationForm.get('isInAllCountry').value == true) {
  //     this._wrapperSearchService.AreaOptions.forEach(area => {
  //       if (area.name == 'כל הארץ') {
  //         if (!this.personalInformation.buisnessAreaList1)
  //           this.personalInformation.buisnessAreaList1 = [];
  //         this.personalInformation.buisnessAreaList1.push({
  //           areaId: area.id
  //         })
  //       }
  //     })
  //   }
  //   //רק אם היא לא בחרה את כל הארץ
  //   else if (this.personalInformationForm.get('isInAllCountry').value == false) {
  //     this.ModelArea.forEach(Marea => {
  //       if (!this.personalInformation.buisnessAreaList1)
  //         this.personalInformation.buisnessAreaList1 = [];
  //       this.personalInformation.buisnessAreaList1.push({
  //         areaId: Marea.id,
  //       });
  //     })
  //   }

  //   //create new buisness
  //   // const formData = new FormData();
  //   // formData.append('Id', null);
  //   // formData.append('userId', localStorage.getItem("logInUserId"));
  //   // formData.append('buisnessName', this.personalInformationForm.get('buisnessName').value);
  //   // formData.append('phoneNumber1', this.personalInformationForm.get('phoneNumber1').value);
  //   // formData.append('phoneNumber2', this.personalInformationForm.get('phoneNumber2').value);
  //   // formData.append('address', this.personalInformationForm.get('address').value);
  //   // formData.append('actionDiscription', this.personalInformationForm.get('actionDiscription').value);
  //   // formData.append('discription', this.personalInformationForm.get('discription').value);
  //   // formData.append('buisnessWebSiteLink', this.personalInformationForm.get('buisnessWebSiteLink').value);
  //   // formData.append('isdisplayBusinessOwnerName', this.personalInformationForm.get('isdisplayBusinessOwnerName').value);
  //   // formData.append('ispayingBuisness', this.personalInformationForm.get('ispayingBuisness').value);
  //   // formData.append('isburterBuisness', this.personalInformationForm.get('isburterBuisness').value);
  //   // formData.append('iscollaborationBuisness', this.personalInformationForm.get('iscollaborationBuisness').value);
  //   // // formData.append('UpdatedBusinessStatus',);
  //   // formData.append('lastupdatedStartDate', null);
  //   // formData.append('CategorySubCategoryList', this.personalInformationForm.get('Category1').value);
  //   // formData.append('CategorySubCategoryBarterList', this.personalInformationForm.get('SubCategory1').value);

  //   // this._buisnessService.createBuisness(this.personalInformation).subscribe(res => {
  //   //   if (res != null) {

  //   //   }
  //   // });

  //   //שליחה לפונק' שיוצרת או מעדכנת את העסק
  //   this._buisnessService.createBuisness(this.personalInformation).subscribe(res => {
  //     if (res != null) {
  //       //מה קורה אם הצליח ליצור או לעדכן?
  //       if (res > 0 && this.personalInformation.id == 0) {
  //         Swal.fire({
  //           icon: 'success',
  //           title: 'יצירת העסק בוצעה בהצלחה!',
  //           html:
  //             'יצירת העסק שלך בוצעה בהצלחה, תוכלי כעת לערוך את העסק שיצרת',
  //           showClass: {
  //             popup:
  //               'עבר בהצלחה'
  //           }
  //         })
  //       }
  //       else if (res > 0 && this.personalInformation.id > 0) {
  //         Swal.fire({
  //           icon: 'success',
  //           title: 'עדכון העסק שלך בוצע בהצלחה!',
  //           html:
  //             'עדכון העסק שלך בוצעה בהצלחה, תוכלי כעת לראות את השינויים בעסק שיצרת ',
  //           showClass: {
  //             popup:
  //               'עבר בהצלחה'
  //           }
  //         })
  //       }
  //     }
  //   },
  //     err => {
  //       //לא הצליח
  //       Swal.fire({
  //         icon: 'error',
  //         title: 'הייתה בעיה ביצירת העסק',
  //         html:
  //           'אנא בדוק שנית שכל הפרטים נכונים',
  //         showClass: {
  //           popup:
  //             'אנא בדוק שוב'
  //         }
  //       })
  //     }
  //   );
  //   // if (this.personalInformation.Id == null) {
  //   //   // this.personalInformation.UpdatedBusinessStatus =
  //   //   this.personalInformation.Id == null;
  //   //   this._buisnessService.createBuisness(this.personalInformation).subscribe(res => {
  //   //     if (res != null) {

  //   //     }
  //   //   });
  //   //update an old buisness
  //   // else

  //   // if (this.personalInformation.Id > 0) {
  //   //todo - return
  //   //   this._buisnessService.updateBuisness(this.personalInformation).subscribe(res => {
  //   //     if (res != null) {

  //   //     }
  //   // });
  //   // }
  // }


  // onSubmitwithFile() {
  //   //todo : 1 add full bus. form to the formdata obj 2 add list of imgs to request 3 set value od profile pic to get curreft img
  //   this.addetionToSubmit();
  //   const formData = new FormData();
  //   formData.append('id', null);
  //   formData.append('userId', localStorage.getItem("logInUserId"));
  //   formData.append('buisnessName', this.personalInformationForm.get('buisnessName').value ? this.personalInformationForm.get('buisnessName').value : null);
  //   formData.append('phoneNumber1', this.personalInformationForm.get('phoneNumber1').value ? this.personalInformationForm.get('phoneNumber1').value : null);
  //   formData.append('phoneNumber2', this.personalInformationForm.get('phoneNumber2').value ? this.personalInformationForm.get('phoneNumber2').value : null);
  //   formData.append('address', this.personalInformationForm.get('address').value ? this.personalInformationForm.get('address').value : null);
  //   formData.append('actionDiscription', this.personalInformationForm.get('actionDiscription').value ? this.personalInformationForm.get('actionDiscription').value : null);
  //   formData.append('discription', this.personalInformationForm.get('discription').value ? this.personalInformationForm.get('discription').value : null);
  //   formData.append('buisnessWebSiteLink', this.personalInformationForm.get('buisnessWebSiteLink').value ? this.personalInformationForm.get('buisnessWebSiteLink').value : null);
  //   formData.append('isdisplayBusinessOwnerName', this.personalInformationForm.get('isdisplayBusinessOwnerName').value ? this.personalInformationForm.get('isdisplayBusinessOwnerName').value : false);
  //   formData.append('ispayingBuisness', this.personalInformationForm.get('ispayingBuisness').value ? this.personalInformationForm.get('ispayingBuisness').value : false);
  //   formData.append('isburterBuisness', this.personalInformationForm.get('isburterBuisness').value ? this.personalInformationForm.get('isburterBuisness').value : false);
  //   formData.append('iscollaborationBuisness', this.personalInformationForm.get('iscollaborationBuisness').value ? this.personalInformationForm.get('iscollaborationBuisness').value : false);
  //   //
  //   formData.append('product1', this.personalInformationForm.get('product1').value ? this.personalInformationForm.get('product1').value : null);
  //   formData.append('product2', this.personalInformationForm.get('product2').value ? this.personalInformationForm.get('product2').value : null);
  //   formData.append('barterProduct1', this.personalInformationForm.get('BarterProduct1').value ? this.personalInformationForm.get('BarterProduct1').value : null);
  //   formData.append('barterProduct2', this.personalInformationForm.get('BarterProduct2').value ? this.personalInformationForm.get('BarterProduct2').value : null);
  //   //  formData.append('UpdatedBusinessStatus', this.personalInformationForm.get('UpdatedBusinessStatus').value ? this.personalInformationForm.get('UpdatedBusinessStatus').value : 0);
  //   //  formData.append('ownerName', this.personalInformationForm.get('ownerName').value ? this.personalInformationForm.get('ownerName').value : null);
  //   //פירוק המערכים
  //   let CatString = '';
  //   this.personalInformation.buisnessCategory1.forEach((item, index) => {
  //     CatString += '[{';
  //     for (let prop in item) {
  //       if (item.hasOwnProperty(prop)) {
  //         CatString += encodeURIComponent("'" + (item as any)[prop]) + "'" + ":";
  //       }
  //     }
  //   })
  //   let catStringToSend = CatString.slice(0, CatString.length - 1);
  //   catStringToSend += '}]';
  //   formData.append("CategoryList1", catStringToSend);
  //   // for (var i = 0; i < this.personalInformation.buisnessCategory1.length; i++) {
  //   //   // formData.append("businessId[" + i + "]",JSON.stringify(this.personalInformation.buisnessCategory1[i].businessId));
  //   //   // formData.append("categoryId[" + i + "]",JSON.stringify(this.personalInformation.buisnessCategory1[i].categoryId));
  //   //   // formData.append("subCategoryId[" + i + "]", JSON.stringify(this.personalInformation.buisnessCategory1[i].subCategoryId));
  //   //   formData.append("combinationtId[" + i + "]", JSON.stringify(this.personalInformation.buisnessCategory1[i].combinationtId));
  //   //   // formData.append("isPossibleInBarter[" + i + "]", JSON.stringify(this.personalInformation.buisnessCategory1[i].isPossibleInBarter));
  //   //   // formData.append("categoryName[" + i + "]", JSON.stringify(this.personalInformation.buisnessCategory1[i].categoryName));
  //   //   // formData.append("subCategoryName[" + i + "]", JSON.stringify(this.personalInformation.buisnessCategory1[i].subCategoryName));
  //   // }
  //   alert(formData.get("CategoryList1"));
  //   formData.append('buisnessCategory1', JSON.stringify(this.personalInformation.buisnessCategory1));
  //   formData.append('buisnessCategory2', JSON.stringify(this.personalInformation.buisnessCategory2));
  //   formData.append('buisnessCategory3', JSON.stringify(this.personalInformation.buisnessCategory3));
  //   formData.append('buisnessCategory4', JSON.stringify(this.personalInformation.buisnessCategory4));
  //   formData.append('buisnessBarterCategory1', JSON.stringify(this.personalInformation.buisnessBarterCategory1));
  //   formData.append('buisnessBarterCategory2', JSON.stringify(this.personalInformation.buisnessBarterCategory2));
  //   formData.append('buisnessAreaList1', JSON.stringify(this.personalInformation.buisnessAreaList1));
  //   formData.append('coverPicture', this.personalInformationForm.get('coverpicfile').value ? this.personalInformationForm.get('coverpicfile').value : null);
  //   formData.append('logoPicture', this.personalInformationForm.get('coverpicfile').value ? this.personalInformationForm.get('coverpicfile').value : null);
  //   // BuisnessPictureVm

  //   const uploadReq = new HttpRequest(
  //     'POST',
  //     `https://localhost:5001/api/Buisness/createBuisnessWithFiles`,
  //     formData,
  //   );

  //   this.httpClient.request(uploadReq).subscribe(event => {
  //     if (event.type === HttpEventType.UploadProgress) {
  //       //this.message = 'Created A new Document Request';
  //       //this.router.navigate(['/infraresearchpage/2']);
  //     } else if (event.type === HttpEventType.Response) {
  //       // this.message = 'Failed Creating A new Document Request';
  //       //this.router.navigate(['/infraresearchpage/2']);
  //     }
  //   });
  // }
  //============================================================================================
  // by efrat
  //הסרה של תמונת תיק עבודות
  // InputWorkRemove(index: number) {
  //   // $('#coverpicfile').trigger('click');
  //   const indexPic: number = this.workPictureGuide.findIndex(g => g.picindex == index);
  //   switch (index) {

  //     case 1:
  //       //remove from html   
  //         $('#1')

  //         .attr('src', " ")
  //       //מחיקת הקובץ מהרשימה
  //       this.uploadedWorksFiles[0] = null;
  //       //מחיקת הגוויאד מהרשימה
  //       if (indexPic !== -1) {
  //         this.workPictureGuide.splice(indexPic, 1);
  //       }
  //       this.workHasImg1 = false;
  //        //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //       break;
  //     case 2:
  //       //remove from html        
  //       $('#2')

  //         .attr('src', " ")
  //       //מחיקת הקובץ מהרשימה
  //       this.uploadedWorksFiles[1] = null;
  //       //מחיקת הגוויאד מהרשימה
  //       if (indexPic !== -1) {
  //         this.workPictureGuide.splice(indexPic, 1);
  //       }
  //       this.workHasImg2 = false;
  //        //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //       break;
  //     case 3:
  //       //remove from html        
  //       $('#3')

  //         .attr('src', " ")
  //       //מחיקת הקובץ מהרשימה
  //       this.uploadedWorksFiles[2] = null;
  //       //מחיקת הגוויאד מהרשימה
  //       if (indexPic !== -1) {
  //         this.workPictureGuide.splice(indexPic, 1);
  //       }
  //       this.workHasImg3 = false;
  //       //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //       break;
  //     case 4:
  //       //remove from html        
  //       $('#4')
  //         // .find('img')
  //         .attr('src', " ")
  //       //מחיקת הקובץ מהרשימה
  //       this.uploadedWorksFiles[3] = null;
  //       //מחיקת הגוויאד מהרשימה
  //       if (indexPic !== -1) {
  //         this.workPictureGuide.splice(indexPic, 1);
  //       }
  //       this.workHasImg4 = false;
  //       //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //       break;
  //     case 5:
  //       //remove from html        
  //       $('#5')
  //        //  .find('img')
  //         .attr('src', " ")
  //       //מחיקת הקובץ מהרשימה
  //       this.uploadedWorksFiles[4] = null;
  //       //מחיקת הגוויאד מהרשימה
  //       if (indexPic !== -1) {
  //         this.workPictureGuide.splice(indexPic, 1);
  //       }
  //       this.workHasImg5 = false;
  //       //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //       break;
  //       case 6:
  //         //remove from html        
  //         $('#6')
  //           //  .find('img')
  //           .attr('src', " ")
  //         //מחיקת הקובץ מהרשימה
  //         this.uploadedWorksFiles[5] = null;
  //         //מחיקת הגוויאד מהרשימה
  //         if (indexPic !== -1) {
  //           this.workPictureGuide.splice(indexPic, 1);
  //         }
  //         this.workHasImg6 = false;
  //         //לעדכן את הflag של הצגת הדיב של העלאת התמונה
  //         break;
  //     default:
  //       break;
  //   }

  // }
  //==================================================
  //hedva
  //custom validation
  // profileCompletionPercentage(formControl: AbstractControl): ValidatorFn | null {
  //   if (formControl.value != null && formControl.value != "")
  //     if (!formControl.pristine)
  //       if (formControl.status == "VALID") {
  //         this.progressBarValue += 10;
  //       }
  //   return null;
  // }
  //================================================
  //hedva
  // clean() {
  //   this.personalInformationForm.reset();
  //   // categorydropdown.resetFilter();
  //   // subcategorydropdown.resetFilter();
  // }
  //====================================================
  //for the upload pic - work
  // onUpload(event) {
  //   for (let file of event.files) {
  //     this.uploadedPictures.push(file);
  //   }
  //   // this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  // }
  // onUploadProgress(event) {

  // }
  //======================================================
  //insert the pic
  //   if (this.workPictureGuide != null) {
  //     this.workPictureGuide.forEach(wp => {
  //       var src = `../../../../../assets/BusinessImages/` + result.id + `/Work/` + wp.picindex + `/` + wp.workPicGuide + `/` + wp.workPicGuide + `.jpg`;
  //       $('#' + wp.picindex).attr('src', src);
  //      if (wp.picindex == 1) {
  //           if (wp.workPicGuide == undefined) {
  //             this['workHasImg' + 1] = false;
  //             // this.workHasImg1 = false;
  //             $('#1')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg1 = true;
  //           }

  //         }
  //         else if (wp.picindex == 2) {
  //           if (wp.workPicGuide == undefined) {
  //             this.workHasImg2 = false;
  //             $('#2')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg2 = true;
  //           }
  //         }
  //         else if (wp.picindex == 3) {
  //           if (wp.workPicGuide == undefined) {
  //             this.workHasImg3 = false;
  //             $('#3')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg3 = true;
  //           }

  //         }
  //         else if (wp.picindex == 4) {
  //           if (wp.workPicGuide == undefined) {
  //             this.workHasImg4 = false;
  //             $('#4')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg4 = true;
  //           }

  //         }

  //         else if (wp.picindex == 5) {
  //           if (wp.workPicGuide == undefined) {
  //             this.workHasImg5 = false;
  //             $('#5')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg5 = true;
  //           }
  //         }
  //         else if (wp.picindex == 6) {
  //           if (wp.workPicGuide == undefined) {
  //             this.workHasImg6 = false;
  //             $('#6')
  //               .attr('src', " ");
  //           }
  //           else {
  //             this.workHasImg6 = true;
  //           }
  //         }
  //     })
  //   } 
  // onChangeSubCategory2() {
  //   this.ModelSubCategory1 = [...new Set(this.ModelSubCategory1)];//מוריד כפולים
  //   this.SubCategoryListForBurter1 = this.ModelSubCategory1;
  // }    
  // onChangeSubCategory3() {
  //   this.SubCategoryListForBurter1 = this.ModelSubCategory1;
  // } 
  // onChangeSubCategory4() {
  //   this.SubCategoryListForBurter4 = this.ModelSubCategory4;
  // }  
  //=============================================
  //~~~~~~~~~~~~~~~~~~~~~באיפוס~~~~~~~~~~~~~~~~
  // this.Category1 = { label: null, value: null };
  // this.Category2 = { label: null, value: null };
  // this.Category3 = { label: null, value: null };
  // this.Category4 = { label: null, value: null };

  // this.personalInformationForm.get('Category1').setValue(this.Category1);//קטגוריה 1
  // this.personalInformationForm.get('Category2').setValue(this.Category2);//קטגוריה 2
  // this.personalInformationForm.get('Category3').setValue(this.Category3);//קטגוריה 3
  // this.personalInformationForm.get('Category4').setValue(this.Category4);//קטגוריה 4
  //איפוס תתי קטגוריות
  // this.ModelSubCategory1 = [];
  // this.ModelSubCategory2 = [];
  // this.ModelSubCategory3 = [];
  // this.ModelSubCategory4 = [];
  // this.ModelSubCategoryForBarter1 = [];
  // this.ModelSubCategoryForBarter2 = [];
  // this.ModelSubCategoryForBarter3 = [];
  // this.ModelSubCategoryForBarter4 = [];
  // this.personalInformationForm.get('SubCategory1').setValue(this.ModelSubCategory1);
  // this.personalInformationForm.get('SubCategory2').setValue(this.ModelSubCategory2);
  // this.personalInformationForm.get('SubCategory3').setValue(this.ModelSubCategory3);
  // this.personalInformationForm.get('SubCategory4').setValue(this.ModelSubCategory4);
  // this.personalInformationForm.get('SubCategoryForBarter1').setValue(this.ModelSubCategory1);
  // this.personalInformationForm.get('SubCategoryForBarter2').setValue(this.ModelSubCategory2);
  // this.personalInformationForm.get('SubCategoryForBarter3').setValue(this.ModelSubCategory3);
  // this.personalInformationForm.get('SubCategoryForBarter4').setValue(this.ModelSubCategory4);
  //איפוס האופציות
  // this._wrapperSearchService.subCategoryOptions1 = [];
  // this._wrapperSearchService.subCategoryOptions2 = [];
  // this._wrapperSearchService.subCategoryOptions3 = [];
  // this._wrapperSearchService.subCategoryOptions4 = [];
  //סגירת הקולאפסים
  // this.isCollapsed1 = true;
  // this.isCollapsed2 = true;
  // this.isCollapsed3 = true;
  // this.isCollapsed4 = true;
