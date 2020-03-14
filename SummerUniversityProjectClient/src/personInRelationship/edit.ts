import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import { PersonInRelationshipService } from "services/person-in-relationship-service";
import { IPersonInRelationshipEdit } from "interfaces/IPersonInRelationshipEdit";
import { IPersonInRelationshipCreate } from "interfaces/IPersonInRelationshipCreate";
import { IRelationship } from "interfaces/IRelationship";
import { RelationshipService } from "services/relationship-service";

export var log = LogManager.getLogger('PersonInRelationship.Edit');

@autoinject
export class Edit {

  private personInRelationshipEdit: IPersonInRelationshipEdit;
  private personInRelationshipEditPut = {} as IPersonInRelationshipCreate;
  private relationShips: IRelationship[] = [];
  private selectedValue: number;

  constructor(private router: Router,
            private relationshipService: RelationshipService,
            private personInRelationshipService: PersonInRelationshipService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit() {
    log.debug('submit', this.personInRelationshipEdit);
    this.personInRelationshipEdit.relationshipId = this.selectedValue;
    this.mapFromEditToCreateInterface();

    this.personInRelationshipService.putCustom(this.personInRelationshipEditPut!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("personsIndex");
        } else {
          alert("Invalid data entered");
          log.error('Error in response!', response);
        }
      }
    );
  }

  mapFromEditToCreateInterface() {
    this.personInRelationshipEditPut.id = this.personInRelationshipEdit.id;
    this.personInRelationshipEditPut.person1Id = this.personInRelationshipEdit.person1.id;
    this.personInRelationshipEditPut.personId = this.personInRelationshipEdit.person.id;
    this.personInRelationshipEditPut.relationshipId = this.personInRelationshipEdit.relationshipId;
  }

  populateRelationDropDown() {
    this.relationshipService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        this.relationShips = jsonData;
      }
    )
  }

  loadData(id: number): void {
    this.personInRelationshipService.fetchForEdit(id).then(
        personInRelationship => {
        log.debug('personInRelationship', personInRelationship);
        this.personInRelationshipEdit = personInRelationship;
        this.selectedValue = this.personInRelationshipEdit.relationship.id;
      });
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
    this.loadData(params.id);
    this.populateRelationDropDown();
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}