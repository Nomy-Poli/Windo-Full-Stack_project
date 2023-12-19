import { AbstractControl } from '@angular/forms';

export function ValidateArrLenght2(control: AbstractControl) {
    if (control.value ) {
        if (control.value.length <= 1) {
            return { noTwoBusiness: true };
        }
    }
    return null;
}