import { cutTextPipe } from './short-text.pipe';

describe('cutTextPipe', () => {
  it('create an instance', () => {
    const pipe = new cutTextPipe();
    expect(pipe).toBeTruthy();
  });
});
