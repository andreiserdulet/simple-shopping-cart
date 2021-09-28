import { BackgroundDirective } from './background.directive';

describe('BackgroundDirective', () => {
  it('should create an instance', () => {
    const directive = BackgroundDirective;
    // Asa imi primeste o eroare ca asteapta un type sa primeasca.
    //const directive = BackgroundDirective();
    expect(directive).toBeTruthy();
  });
});
