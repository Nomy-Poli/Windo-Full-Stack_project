import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cutText'
})
export class cutTextPipe implements PipeTransform {

  transform(value: string, lenght: number): string {
    if(value){
      if(value.length> lenght){
        let str = value.slice(0,lenght-1)+ '...';
        return str;
      }
      return value;
    }
    return '';
  }

}
