import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'elipsis'
})
export class ElipsisPipe implements PipeTransform {

  transform(description: string, length: number): string {
    return description.length > length ?  description.substr(0,length)+'...' : description;
  }

}
