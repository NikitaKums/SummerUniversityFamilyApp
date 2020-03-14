import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import { IFamilyTree } from "interfaces/IFamilyTree";

export var log = LogManager.getLogger('FamilyTreeCreator.Index');

@autoinject
export class FamilyTreeCreator {
  
  private wrapperDiv = document.createElement('div');

  constructor() {
    log.debug('constructor');
    this.wrapperDiv.id = 'tree-wrapper';
  } 

  createTree(familyTree: IFamilyTree): HTMLDivElement{
    log.debug("createTree");
    var depth: number;
    depth = 0;
    var currentPerson: IFamilyTree;
    this.wrapperDiv.innerHTML = '';

    var span = this.createSpanWithText(familyTree.firstName + ' ' + familyTree.lastName);
    span.classList.add('root-person');

    this.wrapperDiv.appendChild(span);
    this.parseFamily(familyTree.family, depth);

    return this.wrapperDiv;
  }

  parseFamily(family: [IFamilyTree], depth: number): HTMLDivElement {
    depth = depth + 1;
    log.debug("depth "+ depth);
    var div = this.createDivWithClass('tree-branch lv' + depth);

    family.forEach(personInFamily => {
      var entryDiv = this.createDivEntryWithSpanText(personInFamily.firstName + ' ' + personInFamily.lastName);
      if (personInFamily.family.length > 0){
        entryDiv.appendChild(this.parseFamily(personInFamily.family, depth));
      }
      div.appendChild(entryDiv);
      this.wrapperDiv.appendChild(div);
    });

    return div;
  }

  createDivEntryWithSpanText(text: string) {
    var div = this.createDivWithClass('tree-entry');
    div.appendChild(this.createSpanWithText(text));
    return div;
  }

  createDivWithClass(classname: string) {
    var div = document.createElement('div');
    div.className = classname;
    return div;
  }

  createSpanWithText(text: string): HTMLSpanElement {
    var span = document.createElement('span');
    span.className += 'tree-label';
    span.innerText = text;
    return span;
  }
}