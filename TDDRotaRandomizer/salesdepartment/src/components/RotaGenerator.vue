<template>
    <div id="rota-generator">
        <b-alert :show="dismissCountDown"
                 dismissible
                 variant="info"
                 @dismissed="dismissCountDown=0"
                 @dismiss-count-down="countDownChanged">
            {{this.alertMessage}}
        </b-alert>
        <b-form class="mt-2" v-on:submit.prevent="onSubmit" inline>
            <label class="mr-sm-2" for="inline-form-custom-select-pref">Generate Rota</label>
            <b-form-datepicker v-on:input="enableGenerator" v-model="rotadate" id="datepicker" class="mb-2 mr-sm-2 mb-sm-0"></b-form-datepicker>
            <b-button :disabled=this.buttonDisabled type="submit" variant="success">Generate</b-button>
        </b-form>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'rota-generator',
        data() {
            return {
                dismissSecs: 5,
                dismissCountDown: 0,
                showDismissibleAlert: false,
                buttonDisabled: true,
                rotadate: null,
                alertMessage: null
            }
        },
        methods: {
            enableGenerator() {
                this.buttonDisabled = false
            },
            countDownChanged(dismissCountDown) {
                this.dismissCountDown = dismissCountDown
            },
            showAlert(message) {
                this.dismissCountDown = this.dismissSecs;
                this.alertMessage = message;
            },
            onSubmit() {
                axios.post('https://localhost:44352/api/rotas/',
                    {
                        "Start": this.rotadate
                    })
                    .then(this.showAlert("Rota created"))
                    .catch((error) => {
                        this.showAlert(error.response.data)
                    });

            }
        }
    }
</script>

<style scoped></style>