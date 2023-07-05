import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AuthorizationService } from '../../services/authorization.service';
import { IUserAuthorizationModel } from '../../models/user-auth.model';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
    selector: 'log-in',
    templateUrl: './log-in.component.html',
    styleUrls: ['./log-in.component.css'],
    providers: [AuthorizationService]
})
export class LogInComponent { 

    constructor(
        public breakpointObserver: BreakpointObserver,
        public authService: AuthorizationService,
        private formBuilder: UntypedFormBuilder
    ) { }


    public validateForm!: UntypedFormGroup;


    ngOnInit(): void {
        this.validateForm = this.formBuilder.group({
            login: [null, [Validators.required]],
            password: [null, [Validators.required]]
        });
    }

    submitForm(): void {
        if(this.validateForm.valid) {
            let authModel = this.validateForm.value as IUserAuthorizationModel;
            this.authService.tryLogIn(authModel);
            return;
        }

        Object.values(this.validateForm.controls)
            .forEach(control => {
                if(control.invalid) {
                    control.markAsDirty();
                    control.updateValueAndValidity({onlySelf: true});
                }
            });
    }
}
