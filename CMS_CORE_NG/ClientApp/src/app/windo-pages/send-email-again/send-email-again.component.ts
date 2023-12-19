import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountNSwagService } from 'src/app/services/AccountNSwag.service';
import { WrapperFuncService } from 'src/app/services/wrapper-func.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-send-email-again',
  templateUrl: './send-email-again.component.html',
  styleUrls: ['./send-email-again.component.css']
})
export class SendEmailAgainComponent implements OnInit {

  constructor(public _wrapperFuncService: WrapperFuncService,
    private _acctNSwag: AccountNSwagService,) { }

  insertForm: FormGroup;
  isLoading = false;
  ngOnInit(): void {

    this.insertForm = new FormGroup({
      Email: new FormControl('',[Validators.required,Validators.email])
    });
  }

  onSubmit(){
    if(this.insertForm.valid){
      this.isLoading = true;
      this._acctNSwag.sendEmailAgain(this.insertForm.value.Email).subscribe(res=>{
        this.isLoading = false;
        console.log(res);
        Swal.fire({
          title:
            'שלחנו לך מייל',
          text:'בדקי את תיבת האימייל שלך, מומלץ לבדוק גם בספאם',
          icon: 'success',
          showClass: {
            popup:
              'אנא בדוק שוב'
          }
        });
      })
    }
  }
}
