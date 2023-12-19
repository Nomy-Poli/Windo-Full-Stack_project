import { AbstractControl } from '@angular/forms';

export function ValidateCountWordMax10(control: AbstractControl) {
    if (control.value) {
        let arr: string[] = control.value.split(' ');
        if (arr.length > 10) {
            return { tooManyWords: true };
        }
    }
    return null;
}
export function ValidateCountWordMax50(control: AbstractControl) {
    if (control.value) {
        let arr: string[] = control.value.split(' ');
        if (arr.length > 50) {
            return { tooManyWords: true };
        }
    }
    return null;
}
export function ValidateCountWordMax70(control: AbstractControl) {
    if (control.value) {
        let arr: string[] = control.value.split(' ');
        if (arr.length > 70) {
            return { tooManyWords: true };
        }
    }
    return null;
}
export function ValidateCountWordMax200(control: AbstractControl) {
    if (control.value) {
        let arr: string[] = control.value.split(' ');
        if (arr.length > 200) {
            return { tooManyWords: true };
        }
    }
    return null;
}
