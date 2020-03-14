import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {IPerson} from "../interfaces/IPerson";
import {PersonsService} from "../services/persons-service";
import { IPersonInRelationshipCreate } from "interfaces/IPersonInRelationshipCreate";
import { IPersonSelect } from "interfaces/IPersonSelect";
import { IRelationship } from "interfaces/IRelationship";
import { PersonInRelationshipService } from "services/person-in-relationship-service";
import { RelationshipService } from "services/relationship-service";

export var log = LogManager.getLogger('Persons.Index');

@autoinject
export class Index {

  private persons: IPerson[] = [];
  private relationShips: IRelationship[] = [];
  private personNames: IPersonSelect[] = [];
  private excludeId: number = 1;
  private selectPersonId: number = null;
  private selectRelationshipId: number = null;
  private personInRelationshipCreate = {} as IPersonInRelationshipCreate;
  private entryCount: number;
  private pageSize: number = 10;
  private pageIndex: number = 1;
  private totalPages: number;

  @bindable private search: string = '';

  constructor(private personsService: PersonsService,
              private relationshipService: RelationshipService,
              private personInRelationshipService: PersonInRelationshipService) {
    log.debug('constructor');
  }

  searchClicked() {
    log.debug('searchClicked', this.search);
    this.loadData();
  }

  searchResetClicked() {
    log.debug('searchResetClicked');
    this.search = '';
    this.loadData();
  }

  addPersonToRelationship(personId: number) {
    log.debug("addPersonToRelationship");
    this.personInRelationshipCreate.person1Id = this.selectPersonId;
    this.personInRelationshipCreate.relationshipId = this.selectRelationshipId;
    this.personInRelationshipCreate.personId = personId;

    this.resetSelect();

    this.personInRelationshipService.postAddPersonToRelationship(this.personInRelationshipCreate).then(
      response => {
        this.personInRelationshipCreate = {} as IPersonInRelationshipCreate
        if (response.status == 201){
          alert("Lisatud");
          this.loadData();
        } else {
          alert("Palun sisestage õiged andmed");
          log.error('Error in response! ', response);
        }
      }
    );
  }

  resetSelect(){
    this.selectPersonId = null;
    this.selectRelationshipId = null;
  }

  previousButtonClicked() {
    log.debug('previousButtonClicked');
    this.pageIndex = this.pageIndex - 1;
    this.loadData();
  }

  nextButtonClicked() {
    log.debug('nextButtonClicked');
    this.pageIndex = this.pageIndex + 1;
    this.loadData();
  }

  loadData() {
    this.personsService.fetchAll('?search=' + this.search, this.pageIndex, this.pageSize).then(
      jsonData => {
        this.persons = jsonData;
        this.populatePersonDropDown();
      }
    );

    this.personsService.getAmountOfEntries('?search=' + this.search).then(
      jsonData => {
        log.debug("entryAmount", jsonData);
        this.entryCount = jsonData;
        this.totalPages = Math.ceil(this.entryCount / this.pageSize);
      }
    );
  }

  populateRelationDropDown() {
    this.relationshipService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        this.relationShips = jsonData;
      }
    )
  }

  populatePersonDropDown() {
    this.personsService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        this.personNames = jsonData;
      }
    );
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
    this.populateRelationDropDown();
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