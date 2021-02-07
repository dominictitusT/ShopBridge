import { Component, EventEmitter, Input, Output } from "@angular/core";


@Component({
    selector: 'app-view',
    templateUrl: './view.component.html',
    
  })
  export class ViewComponent {

    @Input() product :any
    @Output() backbtn: EventEmitter<any> = new EventEmitter();
    href;
    ngOnInit()
    {
        this.href = window.location.href;
    }

    back()
    {
        this.backbtn.emit("1")
    }

  }