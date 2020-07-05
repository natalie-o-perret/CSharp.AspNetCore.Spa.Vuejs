<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-row>
        <v-col>
          <h1 class="headline">SQLite Data</h1>
          <p>This component demonstrates the DX Data Grid fetching data from the server.</p>
          <DxDataGrid :data-source="dataSource" :columns="columns" :show-borders="true" />
        </v-col>
      </v-row>
    </v-slide-y-transition>
    <v-alert :value="showError" type="error" v-text="errorMessage">
      This is an error alert.
    </v-alert>
    <v-alert :value="showError" type="warning">
      Are you sure you're using ASP.NET Core endpoint? (default at
      <a href="http://localhost:5000/sqlite-data">http://localhost:5000</a
      >)
      <br />
      API call would fail with status code 404 when calling from Vue app
      (default at
      <a href="http://localhost:8080/sqlite-data">http://localhost:8080</a>)
      without devServer proxy settings in vue.config.js file.
    </v-alert>
  </v-container>
</template>

<script>
    import DxDataGrid from 'devextreme-vue/data-grid';
    import { customers } from './data.js';
    export default {
        name: 'SqliteData',
        components: {
            DxDataGrid,
        },
        data() {
            return {
                showError: false,
                errorMessage: 'Error while SQLite data.',
                dataSource: customers,
                columns: ['CompanyName', 'City', 'State', 'Phone', 'Fax'],
            };
        },
    };
</script>

<style scoped>

</style>
