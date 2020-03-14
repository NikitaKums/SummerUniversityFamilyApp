import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {IPerson} from "../interfaces/IPerson";
import {PersonsService} from "../services/persons-service";
import { IPersonSelect } from "interfaces/IPersonSelect";
import { IFamilyTree } from "interfaces/IFamilyTree";
import { FamilyTreeCreator } from "helpers/FamilyTreeCreator";

export var log = LogManager.getLogger('FamilyTree.Index');

@autoinject
export class Index {

  private persons: IPerson[] = [];
  private personNames: IPersonSelect[] = [];
  private personRelated: IFamilyTree;
  private selectPersonId: number = null;
  private treeWrapperDiv: HTMLElement;
  private shouldShowFamilyTree: boolean = true;

  constructor(private personsService: PersonsService,
    private familyTreeCreator: FamilyTreeCreator) {
    log.debug('constructor');
  }
  
  loadFamilyTree(){
    this.shouldShowFamilyTree = false;
    this.getPersonsRelatedFamilyMembers();
  }

  getPersonsRelatedFamilyMembers(){
    this.treeWrapperDiv.innerHTML = '';
    log.debug("getPersonsRelatedFamilyMembers " + this.selectPersonId);
    this.personsService.getPersonsRelatedFamilyMembers(this.selectPersonId).then(
      jsonData => {
        log.debug("jsonData " + jsonData);
        this.personRelated = jsonData;
        this.treeWrapperDiv.appendChild(this.familyTreeCreator.createTree(this.personRelated));
        this.shouldShowFamilyTree = true;
        }
    );
  }

  loadData(){
    this.personsService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        this.persons = jsonData;
        this.populatePersonDropDown();
    }
    );
  }

  populatePersonDropDown(){
    this.personNames.length = 0;
    for(var counter:number = 0; counter < this.persons.length; counter++){
      this.personNames.push({
        firstName: this.persons[counter].firstName,
        lastName: this.persons[counter].lastName,
        id: this.persons[counter].id
      });
     }
  }

  // ============ View LifeCycle events ==============
  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
    this.loadData();
    this.treeWrapperDiv = document.getElementById('treeWrapper');
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}