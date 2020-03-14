import {customAttribute, inject} from 'aurelia-framework';
import * as $ from 'jquery';
import * as select2 from 'select2';

@customAttribute('select2')
@inject(Element)
export class Select2CustomAttribute {
  private check: boolean = false;
  constructor(element) {
    // @ts-ignore
    this.element = element;
  }

  attached() {
    // @ts-ignore
    $(this.element).select2(this.value)
    // @ts-ignore
      .on('change', () => {
        console.log(this.check);
        this.check = !this.check;
        if(this.check === true){
          // @ts-ignore
          this.element.dispatchEvent(new Event('change'));
        }
        });
  }

  detached() {
    // @ts-ignore
    $(this.element).select2('destroy');
  }
}