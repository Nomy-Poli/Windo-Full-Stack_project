import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarModule } from "primeng/calendar";

import { UserRoutingModule } from './user-routing.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserSettingsComponent } from './user-settings/user-settings.component';
import { UserActivityComponent } from './user-activity/user-activity.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuardService } from '../services/auth-guard.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    declarations: [UserProfileComponent, UserSettingsComponent, UserActivityComponent],
    imports: [CommonModule, UserRoutingModule, FormsModule, ReactiveFormsModule,CalendarModule],
    providers: [AuthGuardService],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class UserModule { }
