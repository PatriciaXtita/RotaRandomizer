<template>
    <div id="rota-list" class="w-100">
        <b-container class="bv-example-row">
            <b-row>
                <b-col align="center">
                    <h4 class="mt-4">Rotas</h4>
                </b-col>
                <hr />
            </b-row>
            <b-table striped small bordered :items=this.rotas></b-table>
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
        }
    }
</script>

<style scoped></style>