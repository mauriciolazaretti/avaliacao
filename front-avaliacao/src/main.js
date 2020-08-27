import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { VueMaskDirective } from 'v-mask';

Vue.config.productionTip = false
Vue.directive('mask', VueMaskDirective);
new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
