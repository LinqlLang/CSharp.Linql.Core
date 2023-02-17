import { Main } from './app';
process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';

(async () =>
{
    try
    {
        await Main();
    } catch (e)
    {

        console.error(e);
    }
})();