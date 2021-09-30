import { Directive, ElementRef, Input , HostListener} from '@angular/core';

@Directive({
  selector: '[appBackground]'
})
export class BackgroundDirective  {
  @Input() color: string = 'red';

  constructor(private el: ElementRef) { }

  ngOnInit() {
    this.el.nativeElement.style.backgroundColor = this.color;
  }

  @HostListener('mouseenter') onMouseEnter() {
    this.highlight(this.color);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highlight('');
  }

  private highlight(color: string) {
    this.el.nativeElement.style.backgroundColor = color;
  }

}
