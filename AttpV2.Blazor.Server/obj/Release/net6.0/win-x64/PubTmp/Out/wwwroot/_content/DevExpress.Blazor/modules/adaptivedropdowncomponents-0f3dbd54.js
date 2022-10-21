import{registeredComponents as e}from"./dropdowncomponents-8e7e3454.js";import{registeredComponents as t}from"./modalcomponents-25dbcc22.js";import{a as i}from"./_tslib-158249d5.js";import{i as o}from"./logicaltreehelper-15991dcb.js";import{s as r,$ as s}from"./lit-element-b0a6fcba.js";import{e as a}from"./property-d3853089.js";import{n as p}from"./custom-element-bd7061f2.js";import{d as n}from"./events-interseptor-4336a3f0.js";import"./popup-97b8c2f4.js";import"./rect-7fc5c2ef.js";import"./point-9c6ab88a.js";import"./rafaction-bba7928b.js";import"./transformhelper-ebad0156.js";import"./layouthelper-4b19d191.js";import"./positiontracker-dba18a16.js";import"./elementobserver-5f004683.js";import"./supportcaptureelement-35919fa4.js";import"./popuproot-d37b16a2.js";import"./branch-ea431ccc.js";import"./data-qa-utils-8be7c726.js";import"./capturemanager-c7d5aef8.js";import"./simpleevent-84045703.js";import"./locker-c40478e6.js";import"./eventhelper-8570b930.js";import"./dx-ui-element-de378e9d.js";import"./lit-element-base-af247167.js";import"./nameof-factory-64d95f5b.js";import"./custom-events-helper-18f7786a.js";import"./thumb-b78dcc42.js";import"./query-44b9267f.js";import"./popupbasedialog-0778f080.js";import"./popupportal-447465dc.js";import"./browser-a3d50e79.js";import"./dom-95153cd1.js";var d;let l=d=class extends r{constructor(){super(...arguments),this.slotChangedHandler=this.handleSlotChanged.bind(this),this.interceptorSlotChangedHandler=this.handleInterceptorSlotChange.bind(this),this.interceptor=null,this.resizeHandler=this.handleResize.bind(this),this._adaptivityEnabled=!1,this._popupAccessor=null,this.adaptiveWidth=576}get popup(){var e;return(null===(e=this._popupAccessor)||void 0===e?void 0:e.popup)||null}get adaptivityEnabled(){return this._adaptivityEnabled}set adaptivityEnabled(e){this._adaptivityEnabled!==e&&(this._adaptivityEnabled=e,this.raiseEnableAdaptivity(e))}render(){return s`
            <slot @slotchange=${this.slotChangedHandler}></slot>
            <slot name="interceptor" @slotchange=${this.interceptorSlotChangedHandler}></slot>
        `}connectedCallback(){super.connectedCallback(),window.addEventListener("resize",this.resizeHandler,{passive:!0}),setTimeout((()=>this.updateAdaptivity()))}disconnectedCallback(){super.disconnectedCallback(),window.removeEventListener("resize",this.resizeHandler)}handleResize(e){this.updateAdaptivity()}updateAdaptivity(){this.adaptivityEnabled=window.innerWidth<=this.adaptiveWidth}handleSlotChanged(e){const t=e.target.assignedNodes();this._popupAccessor=d.findPopupAccessor(t)}handleInterceptorSlotChange(e){const t=e.target.assignedNodes();this.interceptor=t[0]}raiseEvent(e,t){var i;null===(i=this.interceptor)||void 0===i||i.raise(e,t)}raiseEnableAdaptivity(e){this.raiseEvent("adaptivityChanged",{enabled:e})}static findPopupAccessor(e){const t=e.find((e=>o(e,(()=>["popup","addEventListener","removeEventListener"]))));return t||null}};function c(){}i([a({type:Number,attribute:"adaptive-width"})],l.prototype,"adaptiveWidth",void 0),l=d=i([p("dxbl-adaptive-dropdown")],l);const m={dropDownRegisteredComponents:e,modalRegisteredComponents:t,init:c,dxAdaptiveDropDownTagName:"dxbl-adaptive-dropdown",dxEventsInterceptorTagName:n};export{m as default,c as init};