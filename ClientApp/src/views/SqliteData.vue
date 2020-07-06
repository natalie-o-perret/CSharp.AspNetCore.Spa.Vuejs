<template>
  <v-container fluid>
    <v-slide-y-transition mode="out-in">
      <v-row>
        <v-col>
          <h1 class="headline">SQLite Data</h1>
          <p>This component demonstrates the DX Data Grid fetching data from the server.</p>
          <dx-data-grid
            :data-source="dataSource"
            :remote-operations="true"
            :allow-column-resizing="true"
            :allow-column-reordering="true"
            :column-auto-width="true"
            :column-hiding-enabled="false"
            column-resizing-mode="widget"
            :show-column-lines="true"
            :show-row-lines="true"
            :show-borders="true"
            :hover-state-enabled="true"
            :row-alternation-enabled="true">

            <dx-grouping :context-menu-enabled="true" expand-mode="rowClick" />
            <dx-group-panel :visible="true" :allow-column-dragging="true" />
            <dx-load-panel :enabled="true" :shading="true"/>
            <dx-search-panel :visible="true" />
            <dx-column-fixing :enabled="true"/>
            <dx-column-chooser :enabled="true" mode="select" :allow-search="true" />
            <dx-export :enabled="true" :allow-export-selected-data="true" file-name="Wire Transfers" />
            <dx-paging :enabled="true" :page-size="5" />
            <dx-filter-row :visible="true" />
            <dx-header-filter :visible="true" :allow-search="true" />
            <dx-scrolling :use-native="true" />

            <dx-pager
                    :show-page-size-selector="true"
                    :show-info="true"
                    :allowed-page-sizes="[5, 10, 15]"
                    :show-navigation-buttons="true" />

            <dx-column data-field="id"              caption="Id"  />
            <dx-column data-field="integer"         caption="Integer" />
            <dx-column data-field="real"            caption="Real" />
            <dx-column data-field="nullableText"    caption="Nullable Text" />
            <dx-column data-field="nonNullableText" caption="Non-nullable Text"/>
          </dx-data-grid>
        </v-col>
      </v-row>
    </v-slide-y-transition>
    <v-alert :value="showError" type="error" v-text="errorMessage">
      This is an error alert.
    </v-alert>
    <v-alert :value="showError" type="warning">
      Are you sure you're using ASP.NET Core endpoint? (default at
      <a href="http://localhost:5000/sqlite-data">http://localhost:5000</a>)
      <br />
      API call would fail with status code 404 when calling from Vue app
      (default at
      <a href="http://localhost:8080/sqlite-data">http://localhost:8080</a>)
      without devServer proxy settings in vue.config.js file.
    </v-alert>
  </v-container>
</template>

<script lang="ts">
    import { DxColumn } from 'devextreme-vue/data-grid';
    import { DxFilterPanel } from 'devextreme-vue/data-grid';
    import { DxLookup } from 'devextreme-vue/data-grid';
    import { DxColumnChooser } from 'devextreme-vue/data-grid';
    import { DxColumnFixing } from 'devextreme-vue/data-grid';
    import { DxDataGrid } from 'devextreme-vue/data-grid';
    import { DxExport } from 'devextreme-vue/data-grid';
    import { DxFilterBuilderPopup } from 'devextreme-vue/data-grid';
    import { DxFilterRow } from 'devextreme-vue/data-grid';
    import { DxGrouping } from 'devextreme-vue/data-grid';
    import { DxGroupPanel } from 'devextreme-vue/data-grid';
    import { DxHeaderFilter } from 'devextreme-vue/data-grid';
    import { DxLoadPanel } from 'devextreme-vue/data-grid';
    import { DxPager } from 'devextreme-vue/data-grid';
    import { DxPaging } from 'devextreme-vue/data-grid';
    import { DxScrolling } from 'devextreme-vue/data-grid';
    import { DxSearchPanel } from 'devextreme-vue/data-grid';
    import { DxSelection } from 'devextreme-vue/data-grid';
    import { createStore } from 'devextreme-aspnet-data-nojquery';
    export default {
        name: 'SqliteData',
        components: {
            DxDataGrid,
            DxColumn,
            DxColumnChooser,
            DxColumnFixing,
            DxExport,
            DxFilterBuilderPopup,
            DxFilterPanel,
            DxFilterRow,
            DxGrouping,
            DxGroupPanel,
            DxHeaderFilter,
            DxLoadPanel,
            DxLookup,
            DxPager,
            DxPaging,
            DxScrolling,
            DxSearchPanel,
            DxSelection,
        },
        data() {
            return {
                showError: false,
                errorMessage: 'Error while retrieving SQLite data.',
                dataSource: createStore({
                    key: 'id',
                    loadUrl: `${window.location.origin}/api/v1/sqlite-data`,
                }),
            };
        },
    };
</script>

<style scoped>

</style>
