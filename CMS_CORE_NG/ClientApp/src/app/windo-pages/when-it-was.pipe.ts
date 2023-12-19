import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'whenItWas'
})
export class WhenItWasPipe implements PipeTransform {

  transform(value: Date, withTime?: boolean): string {
    if (value) {
      let temp = new Date(value);
      value = temp;
      const today = new Date()
      const yesterday = new Date;
      yesterday.setDate(today.getDate() - 1);
     
      if (value.toLocaleDateString() == today.toLocaleDateString()) {
        if (value.getHours() == today.getHours()) {
          if (value.getMinutes() == today.getMinutes()) {
            return "עכשיו"
          }
          else if (value.getMinutes() == today.getMinutes() - 1) {
            return "לפני דקה"
          }
          else {
            return `לפני ${today.getMinutes() - value.getMinutes()} דקות`
          }
        }
        else {
          return `לפני ${today.getHours() - value.getHours()} שעות`
        }
      }
      else if (value.toLocaleDateString() == yesterday.toLocaleDateString()) {
        return `אתמול${withTime? ' ('+ value.toLocaleTimeString('he-il',{hour: "2-digit",minute: "2-digit"})+ ') ':''}`;
      }
      let dateOption: DateTimeFormatOptions = {
        day: 'numeric',
        month: 'long',
        year: "numeric",
        hour: withTime ? "2-digit" : undefined,
        minute: withTime ? "2-digit" : undefined
      }
      return value.toLocaleDateString('he-il', dateOption)
    }
    else
      return "";
  }

}
interface DateTimeFormatOptions {
  localeMatcher?: "best fit" | "lookup" | undefined;
  weekday?: "long" | "short" | "narrow" | undefined;
  era?: "long" | "short" | "narrow" | undefined;
  year?: "numeric" | "2-digit" | undefined;
  month?: "numeric" | "2-digit" | "long" | "short" | "narrow" | undefined;
  day?: "numeric" | "2-digit" | undefined;
  hour?: "numeric" | "2-digit" | undefined;
  minute?: "numeric" | "2-digit" | undefined;
  second?: "numeric" | "2-digit" | undefined;
  timeZoneName?: "long" | "short" | undefined;
  formatMatcher?: "best fit" | "basic" | undefined;
  hour12?: boolean | undefined;
  timeZone?: string | undefined;
}