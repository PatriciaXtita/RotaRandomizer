import Vue from 'vue';
import App from './App.vue';
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'


Vue.config.productionTip = true;
Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

import { LayoutPlugin } from 'bootstrap-vue'
Vue.use(LayoutPlugin)
import { BFormDatepicker } from 'bootstrap-vue'
Vue.component('b-form-datepicker', BFormDatepicker)
import { ButtonPlugin } from 'bootstrap-vue'
Vue.use(ButtonPlugin)
import { FormPlugin } from 'bootstrap-vue'
Vue.use(FormPlugin)
import { TablePlugin } from 'bootstrap-vue'
Vue.use(TablePlugin)
import { AlertPlugin } from 'bootstrap-vue'
Vue.use(AlertPlugin)

new Vue({
    render: h => h(App)
}).$mount('#app');
