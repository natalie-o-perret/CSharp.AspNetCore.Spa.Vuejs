import 'core-js/stable';
import 'regenerator-runtime/runtime';
import Vue from 'vue';
import './plugins/axios';
import vuetify from './plugins/vuetify';
import App from './App.vue';
import router from './router';
import store from '@/store/index';
import './registerServiceWorker';
import dateFilter from '@/filters/date.filter';

import '@/styles/dx-data-grid.scss';

// https://devexpress.github.io/ThemeBuilder
// https://js.devexpress.com/Documentation/Guide/Themes_and_Styles/Predefined_Themes/
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.material.blue.light.css';

Vue.config.productionTip = false;

Vue.filter('date', dateFilter);

new Vue({
  vuetify,
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
