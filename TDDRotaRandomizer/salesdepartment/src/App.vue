<template>
    <div id="app">
        <b-container class="bv-example-row">
            <b-row align-v="center">
                <b-col>
                    <h2 class="mt-4 ml-4">Sales Department</h2>
                </b-col>
            </b-row>
            <hr />
            <b-row>
                <b-col cols="9">
                    <b-row>
                        <rota-generator v-on:rotacreated="updateRotas()"></rota-generator>
                    </b-row>
                    <b-row>
                        <shift-menu></shift-menu>
                    </b-row>
                    <b-row>
                        <rota-list ref="rotalist"></rota-list>
                    </b-row>
                </b-col>
                <b-col><employee-list></employee-list></b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script>
    import EmployeeList from './components/EmployeeList.vue';
    import RotaGenerator from './components/RotaGenerator.vue';
    import ShiftMenu from './components/ShiftMenu.vue';
    import RotaList from './components/RotaList.vue';
    import 'bootstrap/dist/css/bootstrap.css'
    import 'bootstrap-vue/dist/bootstrap-vue.css'

    export default {
        name: 'app',
        components: {
            EmployeeList,
            RotaGenerator,
            ShiftMenu,
            RotaList
        },
        methods: {
            updateRotas() {
                this.$refs.rotalist.updateRotas();
            },
        }
    };
</script>

<style>
</style>




