<template>
    <div id="rota-list" class="w-100">
        <b-container class="bv-example-row">
            <b-row>
                <b-col align="center">
                    <h4 class="mt-4">Rotas</h4>
                </b-col>
                <hr />
            </b-row>
            <b-container v-for="rota in rotas">
                <b-row>
                    <b-col>
                        Start: {{rota.start}}
                    </b-col>
                    <b-col>
                        End: {{rota.end}}
                    </b-col>
                </b-row>
                <b-row>
                    <b-table striped small bordered :items=rota.shifts></b-table>
                </b-row>
            </b-container>
           
        </b-container>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'rota-list',
        data() {
            return {
                rotas: [],
            }
        },
        mounted() {
            axios
                .get('https://localhost:44352/api/rotas')
                .then(response => (this.rotas = response.data))
        },
        methods: {
            updateRotas() {
                axios
                    .get('https://localhost:44352/api/rotas')
                    .then(response => (this.rotas = response.data))
            }
        }
    }
</script>

<style scoped></style>